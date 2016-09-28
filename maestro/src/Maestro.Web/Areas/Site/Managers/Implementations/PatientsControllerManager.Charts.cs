using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.DataAccess.EF.Extensions;
using Maestro.Domain;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Microsoft.Ajax.Utilities;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet;
using Maestro.Reporting.Models.PatientTrends;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Helpers;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Devices
    /// </summary>
    public partial class PatientsControllerManager
    {
        private const string MeasurementsCacheKeyTemplate = "measurement_customer{0}_patient{1}";

        /// <summary>
        /// Gets the assessment chart questions.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IList<QuestionInfoViewModel>> GetAssessmentChartQuestions(
            GetAssessmentChartQuestionsViewModel request
        )
        {
            var getHealthSessionsRequest = new SearchHealthSessionsDto()
            {
                PatientId = request.PatientId,
                CompletedFromUtc = request.StartDate,
                CompletedToUtc = request.EndDate,
                ElementType = HealthSessionElementType.Question
            };

            var getHealthSessionsResponse = await GetHealthSessions(getHealthSessionsRequest);

            var healthSessionQuestions = getHealthSessionsResponse
                .Results
                .SelectMany(hs => hs.Elements)
                .Where(el => el.Type == HealthSessionElementType.Question && el.Values.All(v => v.Type != HealthSessionElementValueType.OpenEndedAnswer));

            var result = healthSessionQuestions
                .DistinctBy(q => q.ElementId)
                .Select(q => new QuestionInfoViewModel() { QuestionId = q.ElementId, QuestionText = q.Text });

            return result.ToList();
        }

        /// <summary>
        /// Gets the vitals chart.
        /// </summary>
        /// <param name="getChartRequest">The get chart request.</param>
        /// <returns></returns>
        public async Task<VitalsChartViewModel> GetVitalsChart(GetVitalsChartViewModel getChartRequest)
        {
            // Set date time to end of day, MS-1762: There is no current date and measurement for that day
            getChartRequest.EndDate = getChartRequest.EndDate.AddDays(1).AddMinutes(-1);

            var measurements = await this.GetMeasurements(getChartRequest);
            measurements = measurements.Where(m => m.Vitals.Any(v => v.Name == getChartRequest.Measurement)).ToList();
            
            var resultChart = new VitalsChartViewModel()
            {
                ChartName = getChartRequest.Measurement.Description(),
                ChartRange = new ChartDateRangeViewModel()
                {
                    StartDate = getChartRequest.StartDate,
                    EndDate = getChartRequest.EndDate
                },
                ChartData = new List<BaseChartPointViewModel>(),
                Thresholds = await this.GetVitalChartTresholds(getChartRequest.PatientId, getChartRequest.Measurement)
            };

            if (!measurements.Any())
            {
                return resultChart;
            }

            DateTime endRange = getChartRequest.EndDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone);
            endRange = endRange.AddDays(1).Date.AddMinutes(-1);

            int rangeHours = (int)(endRange - getChartRequest.StartDate).TotalHours;
            int hoursPerPoint = rangeHours / getChartRequest.PointsPerChart;

            if (hoursPerPoint <= 0)
            {
                hoursPerPoint = 24;
            }

            DateTime pointDate = getChartRequest.StartDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone);

            while (pointDate <= endRange)
            {
                DateTime nextPointDate = pointDate.AddHours(hoursPerPoint);

                DateTime startIntervalDate = pointDate;
                DateTime endIntervalDate = DateTimeHelper.Min(nextPointDate, endRange);
                var intervalMeasurements = measurements
                    .Where(v => startIntervalDate <= v.ObservedUtc && v.ObservedUtc <= endIntervalDate)
                    .ToList();

                if (intervalMeasurements.Any())
                {
                    var vitalReadings = new List<VitalReadingViewModel>();

                    foreach (var measurement in intervalMeasurements)
                    {
                        var vitals = measurement.Vitals.Where(v => v.Name == getChartRequest.Measurement).ToList();

                        var readingsToBeAdded = Mapper.Map<IList<VitalBriefResponseDto>, IList<VitalReadingViewModel>>(vitals);

                        var measurementViewModel = Mapper.Map<MeasurementViewModel>(measurement);

                        readingsToBeAdded.Each(r =>
                        {
                            r.Measurement = measurementViewModel;
                            r.Date = measurement.ObservedUtc.ConvertTimeFromUtc(measurement.ObservedTz);
                        });

                        vitalReadings.AddRange(readingsToBeAdded);
                    }

                    var chartPoint = new VitalsChartPointViewModel()
                    {
                        Date = pointDate.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone),
                        Count = vitalReadings.Count(),
                        MinReading = vitalReadings.Min(v => v.Value),
                        MaxReading = vitalReadings.Max(v => v.Value),
                        AvgReading = Math.Round(vitalReadings.Average(v => v.Value), 2),
                        Unit = vitalReadings.Any() ? vitalReadings.ElementAt(0).Unit : string.Empty,
                        Readings = vitalReadings
                    };

                    resultChart.ChartData.Add(chartPoint);
                }

                pointDate = nextPointDate;
            }

            return resultChart;
        }

        private async Task<IList<MeasurementDto>> GetMeasurements(GetVitalsChartViewModel getChartRequest)
        {
            var observerFromUtc = getChartRequest.StartDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone);
            var observerToUtc = getChartRequest.EndDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone);

            var searchVitalsModel = new SearchVitalViewModel()
            {
                PatientId = getChartRequest.PatientId,
                IsInvalidated = false
            };

            PagedResult<MeasurementDto> searchVitalsResponse;

            string cacheKey = string.Format(MeasurementsCacheKeyTemplate, CustomerContext.Current.Customer.Id, getChartRequest.PatientId);

            var measurements = await cacheProvider.Get(
                cacheKey, 
                async () =>
                {
                    searchVitalsModel.ObservedFrom = observerFromUtc;
                    searchVitalsModel.ObservedTo = observerToUtc;

                    searchVitalsResponse = await SearchVitals(searchVitalsModel);

                    return new CacheMeasurementsItem()
                    {
                        DateRange = new ChartDateRangeViewModel(getChartRequest.StartDate, getChartRequest.EndDate),
                        Measurements = searchVitalsResponse.Results.ToList()
                    };
                }
            );

            var incomingDateRange = new ChartDateRangeViewModel(getChartRequest.StartDate, getChartRequest.EndDate);

            //case 1 when incoming date range is in cached date range
            if (incomingDateRange.IsIn(measurements.DateRange))
            {
                return measurements.Measurements;
            }

            //case 2 when cached date range is in incoming date range
            if (measurements.DateRange.IsIn(incomingDateRange))
            {                
                if (incomingDateRange.StartDate.HasValue)
                {
                    searchVitalsModel.ObservedFrom = observerFromUtc;
                }

                if (incomingDateRange.EndDate.HasValue)
                {
                    searchVitalsModel.ObservedTo = observerToUtc;
                }

                searchVitalsResponse = await this.SearchVitals(searchVitalsModel);

                cacheProvider.Add(cacheKey, new CacheMeasurementsItem()
                {
                    DateRange = new ChartDateRangeViewModel(getChartRequest.StartDate, getChartRequest.EndDate),
                    Measurements = searchVitalsResponse.Results.ToList()
                });

                cacheProvider.Add(cacheKey, new CacheMeasurementsItem()
                {
                    DateRange = incomingDateRange,
                    Measurements = searchVitalsResponse.Results.ToList()
                });

                return searchVitalsResponse.Results;
            }

            //case 3 when incoming date range intersects with cached date range
            if (incomingDateRange.IntersectsWith(measurements.DateRange))
            {
                var partOfIncomingDateRange = incomingDateRange.Substract(measurements.DateRange);

                if (partOfIncomingDateRange.StartDate.HasValue)
                {
                    searchVitalsModel.ObservedFrom = observerFromUtc;
                }

                if (partOfIncomingDateRange.EndDate.HasValue)
                {
                    searchVitalsModel.ObservedTo = observerToUtc;
                }
            
                searchVitalsResponse = await this.SearchVitals(searchVitalsModel);

                var updateCacheMeasurements = new List<MeasurementDto>();
                updateCacheMeasurements.AddRange(measurements.Measurements);
                updateCacheMeasurements.AddRange(searchVitalsResponse.Results);

                cacheProvider.Add(cacheKey, new CacheMeasurementsItem()
                {
                    DateRange = ChartDateRangeViewModel.Merge(measurements.DateRange, incomingDateRange),
                    Measurements = updateCacheMeasurements
                });

                return searchVitalsResponse.Results;
            }

            //case 4 when incoming date range does not intersect with caсhed date range        
            var mergedDateRange = ChartDateRangeViewModel.Merge(measurements.DateRange, incomingDateRange);

            if (mergedDateRange.StartDate.HasValue)
            {
                searchVitalsModel.ObservedFrom = observerFromUtc;
            }

            if (mergedDateRange.EndDate.HasValue)
            {
                searchVitalsModel.ObservedTo = observerToUtc;
            }

            searchVitalsResponse = await this.SearchVitals(searchVitalsModel);

            cacheProvider.Add(cacheKey, new CacheMeasurementsItem()
            {
                DateRange = mergedDateRange,
                Measurements = searchVitalsResponse.Results.ToList()
            });

            return searchVitalsResponse.Results;
        }

        /// <summary>
        /// Cleares the measurements cache
        /// </summary>
        /// <returns></returns>
        public void ClearMeasurementsCache(Guid patientId)
        {
            string cacheKey = string.Format(MeasurementsCacheKeyTemplate, CustomerContext.Current.Customer.Id, patientId);

            cacheProvider.Remove(cacheKey);
        }

        /// <summary>
        /// Gets the assessment chart.
        /// </summary>
        /// <param name="getChartRequest">The get chart request.</param>
        /// <returns></returns>
        public async Task<AssessmentChartViewModel> GetAssessmentChart(GetAssessmentChartViewModel getChartRequest)
        {
            var patientTimeZone = PatientContext.Current.Patient.TimeZone;

            var getHealthSessionsRequest = new SearchHealthSessionsDto()
            {
                PatientId = getChartRequest.PatientId,
                CompletedFromUtc = getChartRequest.StartDate.ConvertTimeToUtc(patientTimeZone),
                CompletedToUtc = getChartRequest.EndDate.ConvertTimeToUtc(patientTimeZone)
            };

            var getHealthSessionsResponse = await GetHealthSessions(getHealthSessionsRequest);

            // Get distinct answers
            var token = authDataStorage.GetToken();
            var questionelement = await healthLibraryService
                .GetQuestionElement(
                    token,
                    CustomerContext.Current.Customer.Id,
                    getChartRequest.QuestionId,
                    false
                );

            if (questionelement.AnswerSet is SelectionAnswerSetResponseDto)
            {
                return GetSelectionAnswersChartData(
                    questionelement,
                    getHealthSessionsResponse.Results,
                    patientTimeZone,
                    getChartRequest.StartDate,
                    getChartRequest.EndDate
                );
            }

            if (questionelement.AnswerSet is ScaleAnswerSetResponseDto)
            {
                return GetScaleAnswersChartData(
                    questionelement,
                    getHealthSessionsResponse.Results,
                    patientTimeZone,
                    getChartRequest.StartDate,
                    getChartRequest.EndDate
                );
            }

            return null;
        }

        /// <summary>
        /// Gets the selection answers chart data.
        /// </summary>
        /// <param name="questionElement">The questionelement.</param>
        /// <param name="healthSessions">The health sessions.</param>
        /// <param name="patientTimeZone">The patient time zone.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        private AssessmentChartViewModel GetSelectionAnswersChartData(
            QuestionElementResponseDto questionElement,
            IList<HealthSessionResponseDto> healthSessions,
            string patientTimeZone,
            DateTime startDate,
            DateTime endDate
        )
        {
            var answerChoices = ((SelectionAnswerSetResponseDto)questionElement.AnswerSet).SelectionAnswerChoices;

            var healthSessionQuestionElements = healthSessions
                .SelectMany(hs => hs.Elements)
                .Where(el => el.Type == HealthSessionElementType.Question)
                .Where(qel => qel.ElementId == questionElement.Id)
                .ToList();

            var answerDates = healthSessionQuestionElements.Select(qel => qel.AnsweredUtc).Distinct();

            var chartData = new List<BaseChartPointViewModel>();

            foreach (var answerDate in answerDates)
            {
                var chartPoint = new AssessmentChartPointViewModel()
                {
                    Date = answerDate.HasValue ? answerDate.Value.ConvertTimeFromUtc(patientTimeZone) : DateTimeOffset.MinValue,
                    Values = new List<BaseAnswerStatisticViewModel>()
                };

                var questionsPerPoint = healthSessionQuestionElements.Where(qel => qel.AnsweredUtc == answerDate).ToList();
                var answersPerPoint = questionsPerPoint.SelectMany(qel => qel.Values).Cast<SelectionAnswerDto>().ToList();

                foreach (var answerChoice in answerChoices)
                {
                    chartPoint.Values.Add(new SelectionAnswerStatisticViewModel()
                    {
                        AnswerId = answerChoice.Id.ToString(),
                        Count = answersPerPoint.Count(a => a.Value == answerChoice.Id),
                        Readings = Mapper.Map<List<QuestionReadingViewModel>>(questionsPerPoint.Where(q => q.AnsweredUtc.HasValue)) ,
                        ReadingsDateRange = new ChartDateRangeViewModel()
                        {
                            StartDate = questionsPerPoint.Min(q => q.AnsweredUtc),
                            EndDate = questionsPerPoint.Max(q => q.AnsweredUtc)
                        }
                    });
                }
                chartData.Add(chartPoint);
            }

            var chartAnswers = answerChoices.Select(ac => new AnswerInfoViewModel() { AnswerId = ac.Id.ToString(), AnswerText = ac.AnswerString.Value }).ToList();

            var chartName = healthSessionQuestionElements.Any() ? healthSessionQuestionElements.First().Text : string.Empty;

            var resultChart = new AssessmentChartViewModel()
            {
                ChartName = chartName,
                ChartRange = new ChartDateRangeViewModel()
                {
                    StartDate = startDate,
                    EndDate = endDate
                },
                ChartData = chartData,
                Answers = chartAnswers
            };

            return resultChart;
        }

        /// <summary>
        /// Gets the scale answers chart data.
        /// </summary>
        /// <param name="questionElement">The questionelement.</param>
        /// <param name="healthSessions">The health sessions.</param>
        /// <param name="patientTimeZone">The patient time zone.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        private AssessmentChartViewModel GetScaleAnswersChartData(
            QuestionElementResponseDto questionElement,
            IList<HealthSessionResponseDto> healthSessions,
            string patientTimeZone,
            DateTime startDate,
            DateTime endDate)
        {
            var healthSessionQuestionElements = healthSessions
                .SelectMany(hs => hs.Elements)
                .Where(el => el.Type == HealthSessionElementType.Question)
                .Where(qel => qel.ElementId == questionElement.Id)
                .ToList();

            var chartName = healthSessionQuestionElements.Any() ? healthSessionQuestionElements.First().Text : string.Empty;

            int highValue = ((ScaleAnswerSetResponseDto)questionElement.AnswerSet).HighValue;
            int lowValue = ((ScaleAnswerSetResponseDto)questionElement.AnswerSet).LowValue;

            var result = new AssessmentChartViewModel()
            {
                Answers = Enumerable.Range(lowValue, highValue - lowValue + 1).Select(n => new AnswerInfoViewModel()
                {
                    AnswerId = n.ToString(),
                    AnswerText = n.ToString()
                }).ToList(),
                ChartName = chartName,
                ChartRange = new ChartDateRangeViewModel()
                {
                    StartDate = startDate,
                    EndDate = endDate
                },
                ChartData = new List<BaseChartPointViewModel>()
            };

            var answerDates = healthSessionQuestionElements.Select(qel => qel.AnsweredUtc).Distinct();

            foreach (var answerDate in answerDates)
            {
                var chartPoint = new AssessmentChartPointViewModel()
                {
                    Date = answerDate.HasValue ? answerDate.Value.ConvertTimeFromUtc(patientTimeZone) : DateTimeOffset.MinValue,
                    Values = new List<BaseAnswerStatisticViewModel>()
                };
                
                var questionElementsPerPoint = healthSessionQuestionElements.Where(qel => qel.AnsweredUtc == answerDate).ToList();
                var answersPerPoint = questionElementsPerPoint.SelectMany(qel => qel.Values).Cast<ScaleAndFreeFormAnswerDto>();

                foreach (var group in answersPerPoint.GroupBy(a => a.Value))
                {
                    var questionsPerGroup = questionElementsPerPoint
                        .Where(q => q.Values.Any(v => ((ScaleAndFreeFormAnswerDto)v).Value == group.Key))
                        .ToList();                                        
                    
                    chartPoint.Values.Add(new SelectionAnswerStatisticViewModel()
                    {
                        AnswerId = group.Key,
                        Count = group.Count(),
                        Readings = Mapper.Map<List<QuestionReadingViewModel>>(questionsPerGroup.Where(q => q.AnsweredUtc.HasValue)),
                        ReadingsDateRange = new ChartDateRangeViewModel()
                        {
                            StartDate = questionsPerGroup.Min(q => q.AnsweredUtc),
                            EndDate = questionsPerGroup.Max(q => q.AnsweredUtc)
                        }
                    });
                }

                result.ChartData.Add(chartPoint);
            }

            return result;
        }

        /// <summary>
        /// Generates settings entities using model data.
        /// Saves settings in db using service.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveTrendsSettings(Guid patientId, TrendsSettingsViewModel model)
        {
            var trendSetting = Mapper.Map<TrendSetting>(model);
            trendSetting.PatientId = patientId;

            await trendsSettingsService.Save(trendSetting);
        }

        /// <summary>
        /// Returns settings of chards for specified patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<TrendsSettingsViewModel> GetTrendsSettings(Guid patientId)
        {
            var result = await trendsSettingsService.GetByPatientId(patientId);

            return Mapper.Map<TrendsSettingsViewModel>(result);
        }

        /// <summary>
        /// Exports the trends to excel.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<Tuple<string, byte[]>> ExportTrendsToExcel(Guid patientId)
        {
            var now = DateTime.UtcNow.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone);
            
            var trendsSettings = await trendsSettingsService.GetByPatientId(patientId);

            var startDate = trendsSettings.StartDate ??
                (trendsSettings.LastDays.HasValue ?
                    now.AddDays(-trendsSettings.LastDays.Value) :
                    now.AddDays(-30));

            var endDate = trendsSettings.EndDate ?? now;

            var vitals = trendsSettings
                .ChartsSettings
                .Where(s => s is VitalChartSetting)
                .Cast<VitalChartSetting>()
                .Select(s => s.VitalName)
                .ToList();

            var questionIds = trendsSettings
                .ChartsSettings
                .Where(s => s is QuestionChartSetting)
                .Cast<QuestionChartSetting>()
                .Select(s => s.QuestionId)
                .ToList();

            // Preparing vitals charts data to be exported
            var searchVitalsModel = new SearchVitalsDto()
            {
                CustomerId = CustomerContext.Current.Customer.Id,
                PatientId = patientId,
                IsInvalidated = false,
                ObservedFrom = startDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone),
                ObservedTo = endDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone)
            };

            var measurementsTask = Task.FromResult<PagedResult<MeasurementDto>>(null);

            if (vitals.Any())
            {
                measurementsTask = vitalsService.GetVitals(searchVitalsModel, authDataStorage.GetToken());
            }

            var patientThresholdsTask = vitalsService.GetThresholds(
                CustomerContext.Current.Customer.Id,
                PatientContext.Current.Patient.Id,
                ThresholdSearchType.Aggregate,
                authDataStorage.GetToken()
            );

            // Preparing assessment charts data to be exported
            var assessmentChartsTasks = new List<Task<AssessmentChartViewModel>>();

            if (questionIds.Any())
            {
                foreach (var questionId in questionIds)
                {
                    assessmentChartsTasks.Add(
                        GetAssessmentChart(
                            new GetAssessmentChartViewModel()
                            {
                                PatientId = patientId,
                                QuestionId = questionId,
                                StartDate = startDate,
                                EndDate = endDate
                            }
                        )
                    );
                }
            }

            var assessmentChartsTask = Task.FromResult<AssessmentChartViewModel[]>(null);

            if (assessmentChartsTasks.Any())
            {
                assessmentChartsTask = Task.WhenAll(assessmentChartsTasks);
            }

            await Task.WhenAll(measurementsTask, patientThresholdsTask, assessmentChartsTask);

            var assessmentChartsData = assessmentChartsTasks
                .Select(t => t.Result)
                .Where(a => a.ChartData.Any())
                .ToList();

            IList<VitalResponseModel> vitalsData = new List<VitalResponseModel>();

            if (measurementsTask.Result != null)
            {
                var measurements = measurementsTask.Result.Results;

                foreach (var measurement in measurements)
                {
                    var measurementVitals = measurement
                        .Vitals
                        .Where(v => vitals.Contains(v.Name))
                        .ToList();

                    // Normalizes all vitals
                    var settings = new VitalSettings();

                    foreach (var vital in measurementVitals)
                    {
                        vitalConverter.Convert(vital, settings);
                    }

                    var vitalsModels = Mapper.Map<IList<VitalBriefResponseDto>, IList<VitalResponseModel>>(measurementVitals);
                    vitalsModels.Each(v => v.Observed = measurement.ObservedUtc.ConvertTimeFromUtc(measurement.ObservedTz));

                    vitalsData.AddRange(vitalsModels);
                }
            }

            using (var stream = new MemoryStream())
            {
                patientTrendsExcelReporter.GenerateReport(
                    new PatientTrendsExcelReportModel()
                    {
                        VitalsData = vitalsData.OrderByDescending(v => v.Observed).ToList(),
                        QuestionsData = Mapper.Map<IList<AssessmentChartViewModel>, IList<QuestionAnswersModel>>(assessmentChartsData),
                        Patient = PatientContext.Current.Patient,
                        Thresholds = patientThresholdsTask.Result
                    }, 
                    stream
                );

                return new Tuple<string, byte[]>(
                    string.Format(
                        "trends-patient-{0}-{1}-{2}-{3}.xlsx",
                        PatientContext.Current.Patient.FirstName,
                        PatientContext.Current.Patient.LastName,
                        startDate.ToString("yyyy_M_d"),
                        endDate.ToString("yyyy_M_d")
                    ),
                    stream.ToArray()
                );
            }
        }

        /// <summary>
        /// Returns thresholds for specified vital type or null if thresholds not configured or configured incorrect.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="vitalType"></param>
        /// <returns></returns>
        public async Task<List<ThresholdViewModel>> GetVitalChartTresholds(Guid patientId, VitalType vitalType)
        {
            var token = authDataStorage.GetToken();
            var customerAlertSeverities =
                await this.vitalsService.GetAlertSeverities(CustomerContext.Current.Customer.Id, token);
            var thresholds = await vitalsService.GetThresholds(
                CustomerContext.Current.Customer.Id, patientId, ThresholdSearchType.Aggregate,
                authDataStorage.GetToken());
            var vitalThresholds = customerAlertSeverities.Any()
                // Thresholds with alert severities matches to customer severities 
                // should be displayed in case if customer is configure alert severities.
                ? thresholds.Where(t =>
                    t.Name == vitalType && t.AlertSeverity != null &&
                    customerAlertSeverities.Any(a => a.Id == t.AlertSeverity.Id))

                // Thresholds without alert severities should be displayed in case
                // if customer is not configure alert severities.
                : thresholds.Where(t => t.Name == vitalType && t.AlertSeverity == null);

                

            return Mapper.Map<List<ThresholdViewModel>>(vitalThresholds);
        }
    }
}