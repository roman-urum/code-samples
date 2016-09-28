using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.DataAccess.Redis;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Managers.Interfaces;
using Maestro.Web.Security;
using Maestro.Domain.Constants;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.Enums.Ordering;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Areas.Site.Models.Dashboard;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Helpers;

using NLog;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// Class DashboardControllerHelper.
    /// </summary>
    public class DashboardControllerHelper : IDashboardControllerHelper
    {
        private const string patientsAlertsCackeKeyTemplate = "patients_alerts_{0}";
        private const string careManagerPatientsCackeKey = "care_manager_patients";

        private static readonly IList<AlertType> DefaultAlertTypes = new List<AlertType>()
        {
            AlertType.VitalsViolation,
            AlertType.ResponseViolation,
            AlertType.Adherence
        };
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IPatientsService patientsService;
        private readonly IAuthDataStorage authDataStorage;
        private readonly IVitalsService vitalsService;    
        private readonly ICacheProvider cache;
        private readonly ICustomerUsersService customerUsersService;
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardControllerHelper" /> class.
        /// </summary>
        /// <param name="patientsService">The patients service.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        /// <param name="vitalsService">The vitals service.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="customerUsersService">The customer users service.</param>
        /// <param name="usersService"></param>
        public DashboardControllerHelper(
            IPatientsService patientsService,
            IAuthDataStorage authDataStorage,
            IVitalsService vitalsService,
            ICacheProvider cache,
            ICustomerUsersService customerUsersService,
            IUsersService usersService
        )
        {
            this.patientsService = patientsService;
            this.authDataStorage = authDataStorage;
            this.vitalsService = vitalsService;
            this.cache = cache;
            this.customerUsersService = customerUsersService;
            this.usersService = usersService;
        }

        /// <summary>
        /// Gets the patients cards with alerts.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>.</returns>
        public async Task<GetPatientsCardsResultViewModel> GetPatientsCards(GetPatientsCardsRequestViewModel request)
        {
            var result = new GetPatientsCardsResultViewModel();
            var authToken = authDataStorage.GetToken();
            int customerId = CustomerContext.Current.Customer.Id;
            var user = request.CareManagerId.HasValue ? await usersService.GetUser(request.CareManagerId.Value) : null;
            var getPatientsResult = await GetCareManagerPatients(user);

            if (!getPatientsResult.Status.HasFlag(GetCareManagerPatientsStatus.Success))
            {
                return result;
            }

            var patients = getPatientsResult.Content;

            var patientIds = patients.Select(p => p.Id).ToList();

            if (!patientIds.Any())
            {
                return result;
            }

            var alerts = await GetBriefAlerts(
                customerId,
                authToken,
                request.SeverityIds,
                request.Types,
                patientIds,
                user
            );

            var allPatientsAlerts = alerts
                .Results
                .SelectMany(r => r.Alerts)
                .ToList();

            var requestedSeverities = allPatientsAlerts
                .Select(a => a.AlertSeverity)
                .Where(cs => (request.SeverityIds == null || request.SeverityIds.Contains(cs.Id)) && cs != null)
                .Distinct()
                .ToList();

            var alertsTypes = allPatientsAlerts
                .Select(a => a.Type)
                .Distinct()
                .OrderByDescending(t => t, new AlertTypeComparer())
                .ToList();

            var total = 0;
            var totalResultAlerts = new List<BaseAlertResponseDto>();

            foreach (var patientAlerts in alerts.Results)
            {
                var patient = patients.First(p => p.Id == patientAlerts.PatientId);
                var patientInfo = Mapper.Map<PatientInfoViewModel>(patient);

                if (!patientAlerts.Alerts.Any())
                {
                    continue;
                }

                patientAlerts.Alerts = patientAlerts.Alerts
                    .OrderByDescending(a => a.AlertSeverity == null ? 0 : a.AlertSeverity.Severity)
                    .ThenByDescending(a => a.OccurredUtc)
                    .ToList();

                var latestAlert = patientAlerts.Alerts.FirstOrDefault();

                var patientCard = new BriefPatientCardViewModel
                {
                    PatientInfo = patientInfo,
                    SeverityOfLatestAlert = latestAlert != null ? Mapper.Map<AlertSeverityResponseDto, AlertSeverityViewModel>(latestAlert.AlertSeverity) : null,
                    Counts = CalculateCardsCounts(patientAlerts.Alerts, requestedSeverities, alertsTypes)
                };

                patientCard.VitalsAlertNumbers = CalculateAlertNumbers(patientCard, requestedSeverities, AlertType.VitalsViolation);
                patientCard.ResponseAlertNumbers = CalculateAlertNumbers(patientCard, requestedSeverities, AlertType.ResponseViolation);
                patientCard.AdherenceAlertNumbers = CalculateAlertNumbers(patientCard, requestedSeverities, AlertType.Adherence);
                
                result.PatientCards.Results.Add(patientCard);
               
                total++;
                totalResultAlerts.AddRange(patientAlerts.Alerts);
            }

            result.PatientCards.Total = total;
            result.Counts = CalculateCardsCounts(totalResultAlerts, requestedSeverities, DefaultAlertTypes);
            result.PatientCards.Results = result
                .PatientCards
                .Results
                .OrderByDescending(c => c, new BriefPatientCardComparer(requestedSeverities))
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            return result;
        }

        /// <summary>
        /// Acknowledges alerts.
        /// </summary>
        /// <param name="alertIds">The list of alert identifiers.</param>
        /// <returns></returns>
        public async Task AcknowledgeAlerts(List<Guid> alertIds)
        {
            var authData = authDataStorage.GetUserAuthData();
            var acknowkedgeDto = new AcknowledgeAlertsRequestDto()
            {
                AcknowledgedBy = authData.UserId,
                AlertIds = alertIds
            };

            await vitalsService.AcknowledgeAlerts(CustomerContext.Current.Customer.Id, acknowkedgeDto, authDataStorage.GetToken());
        }

        /// <summary>
        /// Ignores the reading.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="alertIds">The alerts identifiers.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns>Task.</returns>
        public async Task IgnoreReading(IMaestroPrincipal user, Guid measurementId, List<Guid> alertIds, Guid patientId)
        {
            string token = authDataStorage.GetToken();

            await vitalsService.InvalidateMeasurement(
                CustomerContext.Current.Customer.Id,
                patientId,
                measurementId,
                token
            );


            var acknowkedgeDto = new AcknowledgeAlertsRequestDto()
            {
                AcknowledgedBy = user.Id,
                AlertIds = alertIds
            };

            await vitalsService.AcknowledgeAlerts(CustomerContext.Current.Customer.Id, acknowkedgeDto, token);
        }

        /// <summary>
        /// Gets the patient cards.
        /// </summary>
        /// <param name="request">The request parameteres.</param>
        /// <returns></returns>
        public async Task<IList<FullPatientCardViewModel>> GetPatientCards(GetPatientCardsRequestViewModel request)
        {
            logger.Debug("Start getting patient cards");

            var result = new List<FullPatientCardViewModel>();

            var customerId = CustomerContext.Current.Customer.Id;
            var authToken = authDataStorage.GetToken();
            var searchAlertsRequest = new SearchAlertsDto
            {
                PatientIds = new List<Guid>() { request.PatientId },
                Acknowledged = false,
                SeverityIds = request.SeverityIds,
                Types = request.Types,
                IsBrief = false
            };
            var getAlerts = await vitalsService.GetAlerts(customerId, searchAlertsRequest, authToken);

            logger.Debug("patients alerts were retrieved.");

            if (!getAlerts.Results.Any())
            {
                return result;
            }

            var filteredAlerts = FilterAlertsByBloodPresure(getAlerts.Results.First().Alerts);
            var recentQuestionReadingsTask = GetRecentQuestionReadings();
            var recentVitalReadingsTask = GetRecentVitalReadings();
            var recentAdherencesTask = GetRecentAdherences();

            await Task.WhenAll(recentQuestionReadingsTask, recentVitalReadingsTask, recentAdherencesTask);
            logger.Debug("recent adherences, vital readings and question readings were retrieved.");

            var modelBuilder = new PatientCardModelBuilder(
                recentQuestionReadingsTask.Result,
                recentVitalReadingsTask.Result,
                recentAdherencesTask.Result,
                getAlerts.Results.First().Alerts);

            result.AddRange(filteredAlerts.Select(alertDto => modelBuilder.Build(alertDto)));

            logger.Debug("patient cards were build.");

            result = result.Where(r => r.Reading != null)
                     .OrderByDescending(card => card.Reading.Alert.AlertSeverity == null ? 0 : card.Reading.Alert.AlertSeverity.Severity)
                     .ThenByDescending(card => card.Reading.Alert.Occurred)
                     .Skip(request.Skip)
                     .Take(request.Take)
                    .ToList();

            return result;
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            logger.Debug("clear patients alerts cache");
            var alertsCacheKeyPattern = string.Format(patientsAlertsCackeKeyTemplate, CustomerContext.Current.Customer.Id);
            alertsCacheKeyPattern = string.Concat(alertsCacheKeyPattern, "*");
            cache.RemoveByPattern(alertsCacheKeyPattern);

            logger.Debug("clear caremanager patients cache");
            var caremanagersPatientsKeyPattern = string.Concat(careManagerPatientsCackeKey, "*");
            cache.RemoveByPattern(caremanagersPatientsKeyPattern);
        }

        #region Private methods

        private IList<Tuple<int, int>> CalculateAlertNumbers(BriefPatientCardViewModel briefPatientCard, IList<AlertSeverityResponseDto> severities, AlertType alertType)
        {
            var result = new List<Tuple<int, int>>(); //item1 - severity, item2 - alerts count
            foreach (var severity in severities.OrderByDescending(s => s.Severity))
            {
                var alertsTypeCountInfo = briefPatientCard.Counts.FirstOrDefault(alertCountItem => alertCountItem.AlertType == alertType);

                if (alertsTypeCountInfo != null)
                {
                    var severityCountInfo = alertsTypeCountInfo.AlertSeverityCounts.FirstOrDefault(c => c.Id == severity.Id);
                    if (severityCountInfo != null)
                    {
                        result.Add(new Tuple<int, int>(severity.Severity, severityCountInfo.Count));

                        continue;
                    }
                }
                
                result.Add(new Tuple<int, int>(severity.Severity, 0));

            }

            return result;
        }

        /// <summary>
        /// Returns brief alerts for specified patients.
        /// Applies cache.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="authToken"></param>
        /// <param name="severityIds"></param>
        /// <param name="alertTypes"></param>
        /// <param name="patientsIds"></param>
        /// <param name="user">This is only for building the cache key</param>
        /// <returns></returns>
        private async Task<PagedResult<PatientAlertsDto>> GetBriefAlerts(
            int customerId,
            string authToken,
            IList<Guid> severityIds,
            IList<AlertType> alertTypes,
            IList<Guid> patientsIds,
            User user)
        {
            var cacheKey = string.Format(patientsAlertsCackeKeyTemplate, customerId);
            if (user != null)
            {
                cacheKey = string.Concat(cacheKey, user.Id.ToString());
            }

            var searchAlertsRequest = new SearchAlertsDto
            {
                PatientIds = patientsIds,
                Acknowledged = false,
                Types = alertTypes,
                IsBrief = true
            };

            var getPatientsAlertsResult =
                await cache.Get(cacheKey, () => vitalsService.GetAlerts(customerId, searchAlertsRequest, authToken));

            var filteredAlertsFromCache = getPatientsAlertsResult.Results
                .Where(pa => patientsIds == null || !patientsIds.Any() || patientsIds.Contains(pa.PatientId))
                .Select(pa => new PatientAlertsDto
                {
                    PatientId = pa.PatientId,
                    Alerts =
                        pa.Alerts.Where(
                            a =>
                                (severityIds == null || !severityIds.Any() || severityIds.Contains(a.AlertSeverity.Id)) &&
                                (alertTypes == null || !alertTypes.Any() || alertTypes.Contains(a.Type)))
                            .ToList()
                }).ToList();

            return new PagedResult<PatientAlertsDto>()
            {
                Results = filteredAlertsFromCache.ToList(),
                Total = getPatientsAlertsResult.Results.Count
            };
        }

        private IList<AlertCountViewModel> CalculateCardsCounts(
            IList<BaseAlertResponseDto> alerts,
            IList<AlertSeverityResponseDto> severities,
            IList<AlertType> alertTypes)
        {
            var result = new List<AlertCountViewModel>();

            foreach (var alertType in alertTypes)
            {
                var alertCount = new AlertCountViewModel()
                {
                    AlertType = alertType,
                    AlertSeverityCounts = new List<AlertSeverityCountViewModel>(),
                    AlertTypeCount = alerts.Count(a => a.Type == alertType)
                };

                result.Add(alertCount);

                AlertSeverityResponseDto highestSeverity = null;

                foreach (var severity in severities.OrderByDescending(s => s.Severity))
                {
                    var alertsWithThatSeverity = alerts.Where(a => a.Type == alertType && a.AlertSeverity != null && a.AlertSeverity.Id == severity.Id);

                    var severityCount = new AlertSeverityCountViewModel()
                    {
                        Id = severity.Id,
                        Name = severity.Name,
                        ColorCode = severity.ColorCode,
                        Severity = severity.Severity,
                        Count = alertsWithThatSeverity.Count()
                    };

                    alertCount.AlertSeverityCounts.Add(severityCount);

                    if (severityCount.Count > 0)
                    {
                        if (highestSeverity == null || severity.Severity > highestSeverity.Severity)
                        {
                            highestSeverity = severity;
                        }
                    }
                }

                if (highestSeverity != null)
                {
                    var alertsWithHighestSeverity = alerts.Where(a => a.AlertSeverity.Id == highestSeverity.Id).ToList();

                    if (alertsWithHighestSeverity.Any())
                    {
                        alertCount.MostRecentDateOfHighestSeverityUtc = alertsWithHighestSeverity
                            .Max(a => a is VitalAlertResponseDto ?
                                ((VitalAlertResponseDto)a).Measurement.ObservedUtc :
                                (a is HealthSessionElementAlertResponseDto ? ((HealthSessionElementAlertResponseDto)a).AnsweredUtc : a.OccurredUtc)
                            );
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns cached list of patients.
        /// Loads patients from API when cache expires.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Task<OperationResultDto<IList<PatientDto>, GetCareManagerPatientsStatus>> GetCareManagerPatients(User user)
        {            
            var cackeKey = user == null
                ? careManagerPatientsCackeKey
                : string.Concat(careManagerPatientsCackeKey, user.Id);

            return cache.Get(cackeKey, () => LoadCareManagerPatients(user));
        }

        private async Task<OperationResultDto<IList<PatientDto>, GetCareManagerPatientsStatus>> LoadCareManagerPatients(
            User user)
        {
            var authToken = authDataStorage.GetToken();
            int customerId = CustomerContext.Current.Customer.Id;
            IList<PatientDto> patients;
            var searchPatientsRequest = new PatientsSearchDto()
            {
                CustomerId = customerId,
                SiteId = SiteContext.Current.Site.Id
            };

            if (user == null)
            {
                patients = (await patientsService.GetPatients(authToken, searchPatientsRequest)).Results;
            }
            else
            {
                if (user.Role.Name == Roles.SuperAdmin)
                {
                    patients = (await patientsService.GetPatients(authToken, searchPatientsRequest)).Results;
                }
                else
                {
                    var customerUserRoles = await customerUsersService.GetCustomerUserPermissions(user.Id);

                    if (customerUserRoles != null && customerUserRoles.Permissions.Any(p => p.CustomerUserRole.Name == CustomerUserRoles.ManageAllPatients || p.CustomerUserRole.Name == CustomerUserRoles.CustomerAdmin))
                    {
                        searchPatientsRequest.CareManagerId = user.Id;
                        patients = (await patientsService.GetPatients(authToken, searchPatientsRequest)).Results;
                    }
                    else
                    {
                        searchPatientsRequest.CareManagerId = authDataStorage.GetUserAuthData().UserId;
                        patients = (await patientsService.GetPatients(authToken, searchPatientsRequest)).Results;
                    }
                }
            }

            return new OperationResultDto<IList<PatientDto>, GetCareManagerPatientsStatus>()
            {
                Status = GetCareManagerPatientsStatus.Success,
                Content = patients
            };
        }

        private IList<BaseAlertResponseDto> FilterAlertsByBloodPresure(IList<BaseAlertResponseDto> alerts)
        {
            var resultAlerts = new List<BaseAlertResponseDto>();
            var processedMeasurementIds = new HashSet<Guid>();

            foreach (var alert in alerts)
            {
                var vitalAlert = alert as VitalAlertResponseDto;

                if (vitalAlert != null && (vitalAlert.Name == VitalType.SystolicBloodPressure || vitalAlert.Name == VitalType.DiastolicBloodPressure))
                {                    
                    if (!processedMeasurementIds.Contains(vitalAlert.Measurement.Id))
                    {
                        resultAlerts.Add(vitalAlert);
                        processedMeasurementIds.Add(vitalAlert.Measurement.Id);
                    }
                }
                else
                {
                    resultAlerts.Add(alert);
                }
            }

            return resultAlerts;
        }

        private async Task<IList<VitalReadingViewModel>> GetRecentVitalReadings()
        {
            //var cacheKey = string.Format(recentVitalReadingsCackeKeyTemplate, PatientContext.Current.Patient.Id);
            //var valueFromCache = await cache.GetAsync<List<VitalReadingViewModel>>(cacheKey);

            //if (valueFromCache != null)
            //{
            //    logger.Debug("recent vital readings found in the cache");

            //    return valueFromCache;
            //}

            //logger.Debug("recent vital readings not found in the cache");

            var observedTo = DateTime.UtcNow;
            var observedFrom = observedTo.AddDays(-30);

            var searchVitalRequest = new SearchVitalsDto()
            {
                PatientId = PatientContext.Current.Patient.Id,
                CustomerId = CustomerContext.Current.Customer.Id,
                Skip = 0,
                Take = int.MaxValue,
                IsInvalidated = false,
                ObservedFrom = observedFrom,
                ObservedTo = observedTo
            };
            
            var getVitalsResult = await vitalsService.GetVitals(searchVitalRequest, authDataStorage.GetToken());

            var result = new List<VitalReadingViewModel>();

            foreach (var measurement in getVitalsResult.Results)
            {
                var vitalReadings = measurement.Vitals.Select(v =>
                {
                    var reading = Mapper.Map<VitalReadingViewModel>(v);
                    reading.Measurement = Mapper.Map<MeasurementViewModel>(measurement);
                    reading.Date = measurement.ObservedUtc.ConvertTimeFromUtc(measurement.ObservedTz);

                    return reading;
                });

                result.AddRange(vitalReadings);
            }

            //logger.Debug("put recent vital readings to the cache");

            //cache.Add(cacheKey, result);

            return result;
        }

        private async Task<IList<QuestionReadingViewModel>> GetRecentQuestionReadings()
        {
            //var cacheKey = string.Format(recentQuesionReadingsCacheKeyTemplate, PatientContext.Current.Patient.Id);
            //var valueFromCache = await cache.GetAsync<List<QuestionReadingViewModel>>(cacheKey);

            //if (valueFromCache != null)
            //{
            //    logger.Debug("recent qestion readings found in the cache");

            //    return valueFromCache;
            //}

            //logger.Debug("recent qestion readings not found in the cache");
            
            var result = new List<QuestionReadingViewModel>();

            var completedToUtc = DateTime.UtcNow;
            var completedFromUtc = completedToUtc.AddDays(-30);

            var getHealthSessionsRequest = new SearchHealthSessionsDto()
            {
                CustomerId = CustomerContext.Current.Customer.Id,
                PatientId = PatientContext.Current.Patient.Id,
                CompletedFromUtc = completedFromUtc,
                CompletedToUtc = completedToUtc,
                ElementType = HealthSessionElementType.Question
            };

            var getHealthSessionsResponse = await vitalsService.GetHealthSessions(getHealthSessionsRequest, authDataStorage.GetToken());

            if (getHealthSessionsResponse == null || getHealthSessionsResponse.Count <= 0) return result;

            foreach (var healthSession in getHealthSessionsResponse.Results)
            {

                var questionReadings = healthSession.Elements.Where(el => el.Type == HealthSessionElementType.Question).Select(el => Mapper.Map<QuestionReadingViewModel>(el)).ToList();

                result.AddRange(questionReadings);                
            }

            //logger.Debug("put recent qestion readings to the cache");
            //cache.Add(cacheKey, result);

            return result;
        }

        private async Task<IList<AdherenceDto>> GetRecentAdherences()
        {
            var now = DateTime.UtcNow;

            var getAdherencesResult = await patientsService.GetAdherences(
                CustomerContext.Current.Customer.Id,
                PatientContext.Current.Patient.Id,
                new AdherencesSearchDto()
                {
                    OrderBy = AdherenceOrderBy.ScheduledUtc,
                    SortDirection = SortDirection.Ascending,
                    ScheduledAfter = now.AddDays(-30),
                    ScheduledBefore = now,
                    IncludeDeleted = false
                },
                authDataStorage.GetToken()
            );

            return getAdherencesResult.Results;
        }

        #endregion
    }
}