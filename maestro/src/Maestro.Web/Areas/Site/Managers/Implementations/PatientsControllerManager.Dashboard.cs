﻿using System;
using System.Collections.Generic;
﻿using System.IO;
﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Common.Extensions;
﻿using Maestro.Common.Helpers;
﻿using Maestro.DataAccess.EF.Extensions;
﻿using Maestro.Domain;
﻿using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.PatientsService.Calendar;
﻿using Maestro.Domain.Dtos.PatientsService.Enums;
﻿using Maestro.Domain.Dtos.PatientsService.Enums.Ordering;
using Maestro.Domain.Dtos.VitalsService;
﻿using Maestro.Domain.Dtos.VitalsService.Conditions;
﻿using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Domain.Dtos.VitalsService.Enums.Ordering;
﻿using Maestro.Domain.Dtos.VitalsService.Thresholds;
﻿using Maestro.Domain.Enums;
﻿using Maestro.Reporting.Models.PatientDetailedData;
﻿using Maestro.Web.Areas.Site.Models;
﻿using Maestro.Web.Areas.Site.Models.Patients.Charts;
﻿using Maestro.Web.Areas.Site.Models.Patients.Dashboard;
using Maestro.Web.Areas.Site.Models.Patients.DetailedData;
﻿using Maestro.Web.Extensions;
﻿using Maestro.Web.Resources;
﻿using NodaTime;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.Dashboard
    /// </summary>
    public partial class PatientsControllerManager
    {
        private static readonly string HealthSessionGroupDefaultName = GlobalStrings.HealthSessionGroupDefaultName;
        private static readonly string AdhocHealthSessionGroupName = GlobalStrings.AdhocHealthSessionGroupName;

        /// <summary>
        /// Gets the patient specific thresholds.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<PatientSpecificThresholdsViewModel> GetPatientSpecificThresholds(Guid patientId)
        {
            var token = authDataStorage.GetToken();

            var allThresholds = await vitalsService.GetThresholds(
                CustomerContext.Current.Customer.Id,
                patientId,
                ThresholdSearchType.All,
                token
            );

            var patientConditions = await vitalsService.GetPatientConditions(
                CustomerContext.Current.Customer.Id,
                patientId,
                token
            );
            var patientConditionsIds = patientConditions.Select(c => c.Id).ToList();

            return new PatientSpecificThresholdsViewModel()
            {
                Thresholds = allThresholds.OfType<ThresholdDto>().Select(t => t.Name.Description()).Distinct().ToList(),
                ThresholdsOverlap = CheckDefaultThresholdsOverlap(allThresholds.OfType<DefaultThresholdDto>().ToList(), patientConditionsIds)
                                    || CheckPatientThresholdsOverlap(allThresholds.OfType<ThresholdDto>().ToList())
            };
        }

        private bool CheckDefaultThresholdsOverlap(
            IList<DefaultThresholdDto> defaultThresholds,
            IList<Guid> patientConditionsIds)
        {
            var thresholdsGroupedByName = defaultThresholds.GroupBy(t => t.Name);

            foreach (var thresholdGroupByName in thresholdsGroupedByName)
            {
                //If there are no severities a threshold overlap can not be.
                if (thresholdGroupByName.All(t => t.AlertSeverity == null))
                {
                    continue;
                }
              
                var vitalThresholdGroupedBySeverityId = thresholdGroupByName.GroupBy(t => t.AlertSeverity.Id);

                //The list of thresholds per each sesverity
                var effectiveVitalThresholds = vitalThresholdGroupedBySeverityId.Select(group =>
                {
                    var conditionThresholds = group.Where(t => t.DefaultType == ThresholdDefaultType.Condition && t.ConditionId.HasValue && patientConditionsIds.Contains(t.ConditionId.Value)).ToList();
                    if (conditionThresholds.Any())
                    {
                        var minValue = conditionThresholds.Max(t => t.MinValue);
                        var maxValue = conditionThresholds.Min(t => t.MaxValue);

                        return new DefaultThresholdDto()
                        {
                            MinValue = minValue,
                            MaxValue = maxValue
                        };
                    }
                    return group.FirstOrDefault();
                }).ToList();

                if (!effectiveVitalThresholds.Any())
                {
                    continue;
                }

                var highestMinValue = effectiveVitalThresholds.Max(t => t.MinValue);

                if (effectiveVitalThresholds.Any(t => t.MaxValue < highestMinValue))
                {
                    return true;
                }                                                
            }
            

            return false;
        }

        public bool CheckPatientThresholdsOverlap(IList<ThresholdDto> patientThresholds)
        {
            var thresholdsGroupedByName = patientThresholds.GroupBy(t => t.Name);

            foreach (var vitalNameThresholdsGroup in thresholdsGroupedByName)
            {
                //If there are no severities there could not be overlaps
                if (vitalNameThresholdsGroup.All(t => t.AlertSeverity == null))
                {
                    continue;
                }

                var maxOfMin = vitalNameThresholdsGroup.Max(t => t.MinValue);
                if (vitalNameThresholdsGroup.Any(t => maxOfMin > t.MaxValue))
                {
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Gets the peripherals.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<IList<PatientPeripheralViewModel>> GetPeripherals(Guid patientId)
        {
            var token = authDataStorage.GetToken();

            var devices = await devicesService.GetDevices(
                CustomerContext.Current.Customer.Id,
                patientId,
                token
            );

            var filteredByStatusDevices = devices
                .Where(d => d.Status == DeviceStatus.NotActivated || d.Status == DeviceStatus.Activated)
                .ToList();

            var results = new List<PatientPeripheralViewModel>();

            if (filteredByStatusDevices.Any(d => d.Settings.IsWeightAutomated || d.Settings.IsWeightManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.Weight.Description() });
            }

            if (filteredByStatusDevices.Any(d => d.Settings.IsBloodPressureAutomated || d.Settings.IsBloodPressureManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.BloodPressure.Description() });
            }

            if (filteredByStatusDevices.Any(d => d.Settings.IsPulseOxAutomated || d.Settings.IsPulseOxManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.PulseOx.Description() });
            }

            if (filteredByStatusDevices.Any(d => d.Settings.IsBloodGlucoseAutomated || d.Settings.IsBloodGlucoseManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.BloodGlucose.Description() });
            }

            if (filteredByStatusDevices.Any(d => d.Settings.IsPeakFlowAutomated || d.Settings.IsPeakFlowManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.PeakFlow.Description() });
            }

            if (filteredByStatusDevices.Any(d => d.Settings.IsTemperatureAutomated || d.Settings.IsTemperatureManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.Temperature.Description() });
            }

            if (filteredByStatusDevices.Any(d => d.Settings.IsPedometerAutomated || d.Settings.IsPedometerManual))
            {
                results.Add(new PatientPeripheralViewModel() { PeripheralName = PeripheralType.Pedometer.Description() });
            }

            return results;
        }

        /// <summary>
        /// Gets the health sessions dashboard.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<PatientHealthSessionsDashboardViewModel> GetHealthSessionsDashboard(Guid patientId)
        {
            var now = DateTime.UtcNow;
            var token = authDataStorage.GetToken();

            var programsTask = patientsService.GetCalendarPrograms(
                CustomerContext.Current.Customer.Id,
                patientId,
                new BaseSearchDto(),
                token
            );

            var adherencesTask = patientsService.GetAdherences(
                CustomerContext.Current.Customer.Id,
                patientId,
                new AdherencesSearchDto()
                {
                    OrderBy = AdherenceOrderBy.ScheduledUtc,
                    SortDirection = SortDirection.Ascending,
                    ScheduledAfter = now.AddDays(-30),
                    IncludeDeleted = false
                },
                token
            );

            var patientDevicesTask = devicesService.GetDevices(
                CustomerContext.Current.Customer.Id,
                patientId,
                token
            );

            await Task.WhenAll(adherencesTask, programsTask, patientDevicesTask);

            var programs = programsTask.Result.Results;
            var adherences = adherencesTask.Result.Results;
            var patientDevices = patientDevicesTask.Result;

            var lastConnectedDevice = patientDevices
                .Where(d => d.Status == DeviceStatus.Activated)
                .OrderByDescending(d => d.LastConnectedUtc)
                .FirstOrDefault();

            return new PatientHealthSessionsDashboardViewModel()
            {
                NextHealthSessionDate = RetrieveNextHealthSessionDate(adherences, now),
                ActivePrograms = RetrievePatientActiveProgramsNames(programs, now),
                HealthSessionsMissedCount = RetrieveNumberOfHealthSessionsMissed(adherences),
                LastConnectedDate = lastConnectedDevice != null && lastConnectedDevice.LastConnectedUtc.HasValue ?
                    lastConnectedDevice.LastConnectedUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone) :
                    (DateTimeOffset?)null,
                LastConnectedDeviceType = lastConnectedDevice != null ? lastConnectedDevice.DeviceType : string.Empty
            };
        }

        /// <summary>
        /// Gets the latest information dashboard.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<PatientLatestInformationDashboardViewModel> GetLatestInformationDashboard(Guid patientId)
        {
            var searchVitalRequest = new SearchVitalsDto()
            {
                CustomerId = CustomerContext.Current.Customer.Id,
                PatientId = patientId,
                Skip = 0,
                Take = int.MaxValue,
                IsInvalidated = false
            };

            var measurementsForLastThirtyDays = (await vitalsService.GetVitals(searchVitalRequest, authDataStorage.GetToken())).Results;

            var latestHealthSessions = await RetrieveHealthSessionsForLatestCalendarEvent(CustomerContext.Current.Customer.Id, patientId);

            var latestHealthSession = latestHealthSessions.OrderByDescending(hs => hs.CompletedUtc).FirstOrDefault();

            return new PatientLatestInformationDashboardViewModel()
            {
                LatestReadings = CalculateLatestReadings(measurementsForLastThirtyDays),
                LatestHealthSessionReadings = CalculateLatestReadings(measurementsForLastThirtyDays, latestHealthSessions.Select(hs => hs.Id).ToList()),
                LatestHealthSessionQuestionsAndAnswers = CalculateLatestHealthSessionQuestionsAndAnswers(latestHealthSessions),
                LatestHealthSessionDate = latestHealthSession != null ?
                    string.Format(
                        "{0} {1}", 
                        latestHealthSession.CompletedUtc.ConvertTimeFromUtc(latestHealthSession.CompletedTz).DateTime.ToString("f"),
                        latestHealthSession.CompletedTz.GetShortTimezoneName(latestHealthSession.CompletedUtc)
                    ) :
                    string.Empty
            };
        }

        /// <summary>
        /// Gets the grouped health sessions detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        public async Task<IList<HealthSessionDetailedDataGroupViewModel>> 
            GetGroupedHealthSessionsDetailedData(SearchDetailedDataViewModel searchModel)
        {
            var searchHealthSessionsQuery = Mapper.Map<SearchDetailedDataViewModel, SearchHealthSessionsDto>(searchModel);

            var healthSessions = (await this.GetHealthSessions(searchHealthSessionsQuery)).Results;

            var groupedHealthSessions = healthSessions.GroupBy(hs => new
            {
                CalendarItemId = hs.CalendarItemId ?? (Guid?)hs.Id
            });

            var searchCalendarItemsCriteria = new CalendarItemsSearchDto();

            if (searchModel.ObservedFromUtc.HasValue)
            {
                searchCalendarItemsCriteria.ScheduledAfter = searchModel.ObservedFromUtc.Value;
            }

            if (searchModel.ObservedToUtc.HasValue)
            {
                searchCalendarItemsCriteria.ScheduledBefore = searchModel.ObservedToUtc.Value;
            }

            var calendarItems = (await patientsService.GetCalendarItems(
                CustomerContext.Current.Customer.Id,
                searchModel.PatientId,
                searchCalendarItemsCriteria,
                authDataStorage.GetToken()
            )).Results;

            var results = new List<HealthSessionDetailedDataGroupViewModel>();

            foreach (var group in groupedHealthSessions)
            {
                var groupHealthSessionElements = group.SelectMany(g => g.Elements).OrderBy(e => e.AnsweredUtc).ToList();

                if (searchModel.ElementType.HasValue)
                {
                    groupHealthSessionElements = groupHealthSessionElements.Where(hse => hse.Type == searchModel.ElementType.Value).ToList();
                }

                if (!groupHealthSessionElements.Any())
                {
                    continue;
                }

                var calendarItemId = group.Key.CalendarItemId;
                string groupName;

                if (calendarItemId.HasValue)
                {
                    var calendarItem = calendarItems.SingleOrDefault(ci => ci.Id == calendarItemId.Value);

                    if (calendarItem != null && !string.IsNullOrEmpty(calendarItem.Name))
                    {
                        groupName = calendarItem.ProgramDay.HasValue ?
                            string.Format("{0} - {1}", calendarItem.Name, calendarItem.ProgramDay.Value) : 
                            calendarItem.Name;
                    }
                    else
                    {
                        groupName = HealthSessionGroupDefaultName;
                    }
                }
                else
                {
                    groupName = AdhocHealthSessionGroupName;
                }

                var lastHealthSession = group.First(hs => hs.CompletedUtc == group.Max(s => s.CompletedUtc));

                results.Add(
                    new HealthSessionDetailedDataGroupViewModel()
                    {
                        CalendarItemId = calendarItemId,
                        Completed = lastHealthSession.CompletedUtc.ConvertTimeFromUtc(lastHealthSession.CompletedTz),
                        Elements = Mapper.Map<IList<HealthSessionElementDto>, IList<HealthSessionDetailedDataGroupElementViewModel>>(
                            FilterHealthSessionElementsFromInvalidatedMeasurements(groupHealthSessionElements)
                        ),
                        Name = groupName
                    }
                );
            }

            return results.OrderBy(r => r.Completed).ToList();
        }

        /// <summary>
        /// Gets the ungrouped health sessions detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        public async Task<IList<HealthSessionDetailedDataGroupElementViewModel>> 
            GetUngroupedHealthSessionsDetailedData(SearchUngroupedHealthSessionDetailedDataViewModel searchModel)
        {
            var searchHealthSessionsQuery = Mapper.Map<SearchUngroupedHealthSessionDetailedDataViewModel, SearchHealthSessionsDto>(searchModel);

            var healthSessions = (await this.GetHealthSessions(searchHealthSessionsQuery)).Results;
            var healthSessionsElements = healthSessions.SelectMany(hs => hs.Elements).ToList();

            if (searchModel != null)
            {
                if (searchModel.QuestionId.HasValue)
                {
                    healthSessionsElements = healthSessionsElements
                        .Where(hse => hse.Type == HealthSessionElementType.Question && hse.ElementId == searchModel.QuestionId.Value)
                        .ToList();
                }
                else if (searchModel.VitalType.HasValue)
                {
                    healthSessionsElements = healthSessionsElements
                            .Where(hse => hse.Type == HealthSessionElementType.Measurement &&
                                hse.Values
                                    .Cast<MeasurementValueResponseDto>()
                                    .Any(v => v.Type == HealthSessionElementValueType.MeasurementAnswer &&
                                        v.Value.Vitals.Any(vi => vi.Name == searchModel.VitalType.Value)))
                            .ToList();
                }
                else if (searchModel.ElementType.HasValue)
                {
                    // Filtering all health session element with type different from "searchModel.ElementType"
                    healthSessionsElements = healthSessionsElements.Where(hse => hse.Type == searchModel.ElementType.Value).ToList();
                }
            }

            healthSessionsElements = FilterHealthSessionElementsFromInvalidatedMeasurements(healthSessionsElements);

            var results = Mapper.Map<IList<HealthSessionElementDto>, IList<HealthSessionDetailedDataGroupElementViewModel>>(healthSessionsElements);

            return results.OrderBy(e => e.Answered).ToList();
        }

        /// <summary>
        /// Gets the adhoc measurements detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        public async Task<IList<MeasurementViewModel>> GetAdhocMeasurementsDetailedData(SearchAdhocMeasurementsDetailedDataViewModel searchModel)
        {
            if (searchModel.ElementType.HasValue &&
                searchModel.ElementType.Value != HealthSessionElementType.Measurement
            )
            {
                return new List<MeasurementViewModel>();
            }

            var searchQuery = Mapper.Map<SearchAdhocMeasurementsDetailedDataViewModel, SearchVitalsDto>(searchModel);

            searchQuery.CustomerId = CustomerContext.Current.Customer.Id;

            var measurements = (await vitalsService.GetVitals(searchQuery, authDataStorage.GetToken())).Results;

            // Filtering measurements from Health Sessions
            measurements = measurements.Where(m => !m.HealthSessionId.HasValue).ToList();

            if (searchModel.VitalType.HasValue)
            {
                measurements = measurements
                    .Where(
                        m => m.Vitals.Any(v => v.Name == searchModel.VitalType.Value)
                    )
                    .ToList();
            }

            return Mapper.Map<IList<MeasurementDto>, IList<MeasurementViewModel>>(measurements);
        }

        /// <summary>
        /// Exports the detailed data to excel.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="groupByHealthSession"></param>
        /// <param name="observedFromUtc">The observed from UTC.</param>
        /// <param name="observedToUtc">The observed to UTC.</param>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="vitalType">Type of the vital.</param>
        /// <param name="questionId">The question identifier.</param>
        /// <returns></returns>
        public async Task<byte[]> ExportDetailedDataToExcel(
            Guid patientId,
            bool groupByHealthSession, 
            DateTime? observedFromUtc = null, 
            DateTime? observedToUtc = null, 
            HealthSessionElementType? elementType = null, 
            VitalType? vitalType = null, 
            Guid? questionId = null
        )
        {
            var reportModel = new PatientDetailedDataExcelReportModel()
            {
                Patient = PatientContext.Current.Patient,
                Elements = new List<DetailedDataRowModel>()
            };

            var adhocMeasurementsDetailedDataTask = GetAdhocMeasurementsDetailedData(
                new SearchAdhocMeasurementsDetailedDataViewModel()
                {
                    PatientId = patientId,
                    ElementType = elementType,
                    ObservedFromUtc = observedFromUtc,
                    ObservedToUtc = observedToUtc,
                    VitalType = vitalType
                }
            );

            if (groupByHealthSession)
            {
                var groupedHealthSessionsDetailedDataTask = GetGroupedHealthSessionsDetailedData(
                    new SearchDetailedDataViewModel()
                    {
                        PatientId = patientId,
                        ElementType = elementType,
                        ObservedFromUtc = observedFromUtc,
                        ObservedToUtc = observedToUtc
                    }
                );

                await Task.WhenAll(adhocMeasurementsDetailedDataTask, groupedHealthSessionsDetailedDataTask);

                foreach (var adhocMeasurement in adhocMeasurementsDetailedDataTask.Result)
                {
                    reportModel.Elements.AddRange(PrepareMeasurementToBeExportedToExcel(adhocMeasurement));
                }

                reportModel.Elements.AddRange(PrepareGroupedDetailedDataToBeExportedToExcel(groupedHealthSessionsDetailedDataTask.Result));
            }
            else
            {
                var ungroupedHealthSessionsDetailedDataTask = GetUngroupedHealthSessionsDetailedData(
                    new SearchUngroupedHealthSessionDetailedDataViewModel()
                    {
                        PatientId = patientId,
                        ElementType = elementType,
                        ObservedFromUtc = observedFromUtc,
                        ObservedToUtc = observedToUtc,
                        QuestionId = questionId,
                        VitalType = vitalType
                    }
                );

                await Task.WhenAll(adhocMeasurementsDetailedDataTask, ungroupedHealthSessionsDetailedDataTask);

                foreach (var adhocMeasurement in adhocMeasurementsDetailedDataTask.Result)
                {
                    reportModel.Elements.AddRange(PrepareMeasurementToBeExportedToExcel(adhocMeasurement));
                }

                reportModel.Elements.AddRange(PrepareDetailedDataElementsToBeExportedToExcel(ungroupedHealthSessionsDetailedDataTask.Result));
            }

            reportModel.Elements = reportModel.Elements.OrderByDescending(e => e.Date).ToList();

            foreach (var groupedDetailedDataRow in reportModel.Elements.Where(e => e is GroupedDetailedDataRowModel).ToList())
            {
                var model = (GroupedDetailedDataRowModel)groupedDetailedDataRow;

                model.GroupElements = model.GroupElements.OrderByDescending(e => e.Date).ToList();
            }

            using (var stream = new MemoryStream())
            {
                patientDetailedDataExcelReporter.GenerateReport(reportModel, stream);

                return stream.ToArray();
            }
        }

        private IList<GroupedDetailedDataRowModel> PrepareGroupedDetailedDataToBeExportedToExcel(
            IList<HealthSessionDetailedDataGroupViewModel> detailedData
        )
        {
            var results = new List<GroupedDetailedDataRowModel>();

            foreach (var element in detailedData)
            {
                var row = new GroupedDetailedDataRowModel()
                {
                    Date = element.Completed,
                    Description = element.Name,
                    Value = string.Empty,
                    GroupElements = new List<DetailedDataRowModel>()
                };

                row.GroupElements.AddRange(PrepareDetailedDataElementsToBeExportedToExcel(element.Elements));

                results.Add(row);
            }

            return results;
        }

        private IList<DetailedDataRowModel> PrepareDetailedDataElementsToBeExportedToExcel(
            IList<HealthSessionDetailedDataGroupElementViewModel> detailedDataElements
        )
        {
            var results = new List<DetailedDataRowModel>();

            foreach (var element in detailedDataElements)
            {
                if (element.Type == HealthSessionElementType.Question)
                {
                    var values = new List<string>();

                    foreach (var value in element.Values)
                    {
                        if (value is SelectionAnswerHealthSessionElementValueViewModel)
                        {
                            var selectionAnswer = (SelectionAnswerHealthSessionElementValueViewModel)value;

                            values.Add(selectionAnswer.Text);
                        }

                        if (value is ScaleAndFreeFormAnswerHealthSessionElementValueViewModel)
                        {
                            var scaleAnswer = (ScaleAndFreeFormAnswerHealthSessionElementValueViewModel)value;

                            values.Add(scaleAnswer.Value);
                        }
                    }

                    results.Add(
                        new DetailedDataRowModel()
                        {
                            Date = element.Answered,
                            Description = element.Text,
                            Value = string.Join(", ", values.OrderBy(v => v, new NaturalSortComparer()))
                        }
                    );
                }

                if (element.Type == HealthSessionElementType.Measurement)
                {
                    foreach (var value in element.Values)
                    {
                        if (value is MeasurementHealthSessionElementValueViewModel)
                        {
                            results.AddRange(PrepareMeasurementToBeExportedToExcel(((MeasurementHealthSessionElementValueViewModel)value).Value));
                        }
                    }
                }

                if (element.Type == HealthSessionElementType.Assessment)
                {
                    var values = new List<string>();

                    foreach (var value in element.Values)
                    {
                        if (value is AssessmentHealthSessionElementValueViewModel)
                        {
                            var assessment = (AssessmentHealthSessionElementValueViewModel)value;

                            values.Add(assessment.AssessmentMedia.AssessmentMediaUrl);
                        }
                    }

                    results.Add(
                        new DetailedDataRowModel()
                        {
                            Date = element.Answered,
                            Description = element.Text,
                            Value = string.Join(", ", values.OrderBy(v => v, new NaturalSortComparer()))
                        }
                    );
                }

                if (element.Type == HealthSessionElementType.TextMedia)
                {
                    results.Add(
                        new DetailedDataRowModel()
                        {
                            Date = element.Answered,
                            Description = element.Text,
                            Value = element.MediaName
                        }
                    );
                }
            }

            return results;
        }

        private IList<DetailedDataRowModel> PrepareMeasurementToBeExportedToExcel(
            MeasurementViewModel measurement
        )
        {
            var results = new List<DetailedDataRowModel>();

            if (measurement.Vitals.Any(v => v.Name.ToLower() == VitalType.SystolicBloodPressure.Description().ToLower() ||
                v.Name.ToLower() == VitalType.DiastolicBloodPressure.Description().ToLower())
            )
            {
                var systolic = measurement.Vitals.FirstOrDefault(v => v.Name.ToLower() == VitalType.SystolicBloodPressure.Description().ToLower());
                var diastolic = measurement.Vitals.FirstOrDefault(v => v.Name.ToLower() == VitalType.DiastolicBloodPressure.Description().ToLower());

                var systolicDiastolicRow = new DetailedDataRowModel()
                {
                    Date = measurement.Observed,
                    Description = "Blood Pressure",
                    Value = string.Format(
                        measurement.IsAutomated ? "{0} / {1} {2}" : "* {0} / {1} {2}",
                        systolic != null ? systolic.Value.ToString("F") : "-",
                        diastolic != null ? diastolic.Value.ToString("F") : "-",
                        systolic != null ? systolic.Unit : (diastolic != null ? diastolic.Unit : string.Empty)
                    )
                };

                results.Add(systolicDiastolicRow);

                if (systolic != null)
                {
                    measurement.Vitals.Remove(systolic);
                }

                if (diastolic != null)
                {
                    measurement.Vitals.Remove(diastolic);
                }
            }

            foreach (var vital in measurement.Vitals)
            {
                var row = new DetailedDataRowModel()
                {
                    Date = measurement.Observed,
                    Description = vital.Name,
                    Value = string.Format(
                        measurement.IsAutomated ? "{0} {1}" : "* {0} {1}",
                        vital.Value,
                        vital.Unit
                    )
                };

                results.Add(row);
            }

            return results;
        }

        /// <summary>
        /// Calculates the latest health session questions and answers.
        /// </summary>
        /// <param name="latestHealthSessions">The latest health sessions.</param>
        /// <returns></returns>
        private IList<QuestionReadingViewModel> CalculateLatestHealthSessionQuestionsAndAnswers(
            IList<HealthSessionResponseDto> latestHealthSessions
        )
        {
            var healthSessionsQuestionElements = latestHealthSessions
                .SelectMany(hs => hs.Elements)
                .Where(e => e.Type == HealthSessionElementType.Question)
                .OrderByDescending(e => e.AnsweredUtc)
                .ToList();

            return Mapper.Map<IList<HealthSessionElementDto>, IList<QuestionReadingViewModel>>(healthSessionsQuestionElements);
        }

        /// <summary>
        /// Retrieves the latest health session.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        private async Task<IList<HealthSessionResponseDto>> RetrieveHealthSessionsForLatestCalendarEvent(
            int customerId,
            Guid patientId
        )
        {
            var searchLatestHealthSession = new SearchHealthSessionsDto()
            {
                CustomerId = customerId,
                PatientId = patientId,
                Skip = 0,
                Take = 1,
                OrderBy = HealthSessionOrderBy.CompletedUtc,
                SortDirection = SortDirection.Descending
            };

            var pagedResult = await vitalsService.GetHealthSessions(searchLatestHealthSession, authDataStorage.GetToken());

            var latestHealthSession = pagedResult.Results.FirstOrDefault();

            if (latestHealthSession != null)
            {
                if (latestHealthSession.CalendarItemId.HasValue)
                {
                    // Retrieving all health sessions related to this calendar event
                    var searchLatestHealthSessions = new SearchHealthSessionsDto()
                    {
                        CustomerId = customerId,
                        PatientId = patientId,
                        CalendarItemId = latestHealthSession.CalendarItemId.Value,
                        OrderBy = HealthSessionOrderBy.CompletedUtc,
                        SortDirection = SortDirection.Descending
                    };

                    return (await vitalsService.GetHealthSessions(searchLatestHealthSessions, authDataStorage.GetToken())).Results;
                }

                return pagedResult.Results;
            }

            return new List<HealthSessionResponseDto>();
        }

        /// <summary>
        /// Calculates the latest readings.
        /// </summary>
        /// <param name="readingsSource">The readings source.</param>
        /// <param name="healthSessionsScope">Calculates readings in scope of provided health sessions.</param>
        /// <returns></returns>
        private IList<VitalReadingWithTrendViewModel> CalculateLatestReadings(
            IList<MeasurementDto> readingsSource,
            IList<Guid> healthSessionsScope = null
        )
        {
            var result = new List<VitalReadingWithTrendViewModel>();
            var readingsSourceOrderedByObservedDate = readingsSource
                .OrderByDescending(m => m.ObservedUtc)
                .ToList();

            foreach (var readingSource in readingsSourceOrderedByObservedDate)
            {
                foreach (var vital in readingSource.Vitals)
                {
                    var latestReading = result.SingleOrDefault(lr => lr.Name.ToLower() == vital.Name.Description().ToLower());

                    if (latestReading != null)
                    {
                        if (latestReading.Trend == TrendType.None)
                        {
                            latestReading.Trend = CalculateTrend(latestReading.Value, vital.Value);
                        }
                    }
                    else
                    {
                        if (
                            healthSessionsScope == null ||
                            (readingSource.HealthSessionId.HasValue && healthSessionsScope.Contains(readingSource.HealthSessionId.Value))
                        )
                        {
                            var vitalReading = Mapper.Map<VitalBriefResponseDto, VitalReadingWithTrendViewModel>(vital);
                            vitalReading.Measurement = Mapper.Map<MeasurementViewModel>(readingSource);
                            vitalReading.Date = readingSource.ObservedUtc.ConvertTimeFromUtc(readingSource.ObservedTz);
                            vitalReading.IsAutomated = readingSource.IsAutomated;

                            result.Add(vitalReading);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the trend.
        /// </summary>
        /// <param name="currentValue">The current value.</param>
        /// <param name="previousValue">The previous value.</param>
        /// <returns></returns>
        private TrendType CalculateTrend(decimal currentValue, decimal previousValue)
        {
            var trend = currentValue - previousValue;

            if (trend == 0)
            {
                return TrendType.WithoutChanges;
            }

            if (trend > 0)
            {
                return TrendType.Increasing;
            }

            return TrendType.Decreasing;
        }

        /// <summary>
        /// Filters the health session elements from invalidated measurements.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        private List<HealthSessionElementDto> FilterHealthSessionElementsFromInvalidatedMeasurements(
            List<HealthSessionElementDto> target
        )
        {
            foreach (var healthSessionsElement in target)
            {
                if (healthSessionsElement.Type == HealthSessionElementType.Measurement)
                {
                    healthSessionsElement.Values.RemoveRange(
                        healthSessionsElement
                            .Values
                            .Where(m => m.Type == HealthSessionElementValueType.MeasurementAnswer)
                            .Cast<MeasurementValueResponseDto>()
                            .Where(m => m.Value.IsInvalidated)
                            .ToList()
                    );
                }
            }

            return target.Where(
                e => e.Type != HealthSessionElementType.Measurement ||
                    (e.Type == HealthSessionElementType.Measurement && e.Values.Any())
            ).ToList();
        }

        private IList<string> RetrievePatientActiveProgramsNames(IList<CalendarProgramResponseDto> sourcePrograms, DateTime currentDateUtc)
        {
            var activePrograms = new List<string>();

            foreach (var program in sourcePrograms)
            {
                if (program.StartDateUtc <= currentDateUtc)
                {
                    var programEndDate = program.StartDateUtc.AddDays(program.EndDay - program.StartDay);

                    if (programEndDate > currentDateUtc)
                    {
                        activePrograms.Add(program.ProgramName);
                    }
                }
            }

            return activePrograms;
        }

        private DateTimeOffset? RetrieveNextHealthSessionDate(IList<AdherenceDto> sourceAdherences, DateTime currentDateUtc)
        {
            var closestHealthSession = sourceAdherences
                .OrderBy(a => a.ScheduledUtc)
                .FirstOrDefault(a => a.ScheduledUtc > currentDateUtc);

            return closestHealthSession != null ? 
                closestHealthSession.ScheduledUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone) :
                (DateTimeOffset?)null;
        }

        private int RetrieveNumberOfHealthSessionsMissed(IList<AdherenceDto> sourceAdherences)
        {
            sourceAdherences = sourceAdherences.OrderBy(a => a.ScheduledUtc).ToList();

            var lastCompletedOrPartiallyCompletedAdherence = sourceAdherences
                .LastOrDefault(
                    a => a.Status == AdherenceStatus.Completed ||
                    a.Status == AdherenceStatus.PartiallyCompleted
                );

            if (lastCompletedOrPartiallyCompletedAdherence == null)
            {
                return sourceAdherences.Count(a => a.Status == AdherenceStatus.Missed);
            }

            var indexOfLastCompletedOrPartiallyCompletedAdherence = sourceAdherences.IndexOf(lastCompletedOrPartiallyCompletedAdherence);

            return sourceAdherences.Skip(indexOfLastCompletedOrPartiallyCompletedAdherence + 1)
                                   .Count(a => a.Status == AdherenceStatus.Missed);
        }
    }
}