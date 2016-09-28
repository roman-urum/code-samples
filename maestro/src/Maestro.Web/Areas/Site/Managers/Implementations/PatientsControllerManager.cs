using System;
using System.Collections.Generic;
using System.Linq;
using Maestro.Domain.Constants;
using Maestro.Domain.Dtos.DevicesService;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Security;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.DevicesService.Enums;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Web.Areas.Site.Managers.Interfaces;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.SearchPatients;
using Maestro.DataAccess.Redis;
using Maestro.Domain.Dtos.MessagingHub;
using Maestro.Domain.Dtos.MessagingHub.Enums;
using Maestro.Reporting.Excel.PatientDetailedData;
using Maestro.Reporting.Excel.PatientTrends;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.
    /// </summary>
    public partial class PatientsControllerManager : IPatientsControllerManager
    {
        private readonly IPatientsService patientsService;
        private readonly IDevicesService devicesService;
        private readonly IAuthDataStorage authDataStorage;
        private readonly IUsersService usersService;
        private readonly ICustomerUsersService customerUsersService;
        private readonly IVitalsService vitalsService;
        private readonly IHealthLibraryService healthLibraryService;
        private readonly ITrendsSettingsService trendsSettingsService;
        private readonly INotesService notesService;
        private readonly IZoomService zoomService;
        private readonly IMessagingHubService messagingHubService;
        private readonly ICacheProvider cacheProvider;
        private readonly IPatientTrendsExcelReporter patientTrendsExcelReporter;
        private readonly IPatientDetailedDataExcelReporter patientDetailedDataExcelReporter;
        private readonly IVitalConverter vitalConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientsControllerManager" /> class.
        /// </summary>
        /// <param name="patientsService">The patients service.</param>
        /// <param name="devicesService">The devices service.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        /// <param name="usersService">The users service.</param>
        /// <param name="customerUsersService">The customer users service.</param>
        /// <param name="vitalsService">The vitals service.</param>
        /// <param name="healthLibraryService">The health library service</param>
        /// <param name="trendsSettingsService">The trends settings service.</param>
        /// <param name="notesService">The note service</param>
        /// <param name="zoomService">The zoom service.</param>
        /// <param name="messagingHubService">The messaging hub service.</param>
        /// <param name="cacheProvider">The cache provider</param>
        /// <param name="patientTrendsExcelReporter">The patient trends excel reporter.</param>
        /// <param name="patientDetailedDataExcelReporter">The patient detailed data excel reporter.</param>
        /// <param name="vitalConverter">The vital converter.</param>
        public PatientsControllerManager(
            IPatientsService patientsService,
            IDevicesService devicesService,
            IAuthDataStorage authDataStorage,
            IUsersService usersService,
            ICustomerUsersService customerUsersService,
            IVitalsService vitalsService,
            IHealthLibraryService healthLibraryService,
            ITrendsSettingsService trendsSettingsService,
            INotesService notesService,
            IZoomService zoomService,
            IMessagingHubService messagingHubService,
            ICacheProvider cacheProvider,
            IPatientTrendsExcelReporter patientTrendsExcelReporter,
            IPatientDetailedDataExcelReporter patientDetailedDataExcelReporter,
            IVitalConverter vitalConverter
        )
        {
            this.patientsService = patientsService;
            this.devicesService = devicesService;
            this.authDataStorage = authDataStorage;
            this.usersService = usersService;
            this.customerUsersService = customerUsersService;
            this.vitalsService = vitalsService;
            this.healthLibraryService = healthLibraryService;
            this.trendsSettingsService = trendsSettingsService;
            this.notesService = notesService;
            this.zoomService = zoomService;
            this.messagingHubService = messagingHubService;
            this.cacheProvider = cacheProvider;
            this.patientTrendsExcelReporter = patientTrendsExcelReporter;
            this.patientDetailedDataExcelReporter = patientDetailedDataExcelReporter;
            this.vitalConverter = vitalConverter;
        }

        /// <summary>
        /// Gets the brief patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<BriefPatientViewModel> GetBriefPatient(Guid patientId)
        {
            var bearerToken = authDataStorage.GetToken();
            var customerId = CustomerContext.Current.Customer.Id;

            var patient = await patientsService.GetPatientAsync(customerId, patientId, false, bearerToken);

            return Mapper.Map<PatientDto, BriefPatientViewModel>(patient);
        }

        /// <summary>
        /// Gets the full patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<FullPatientViewModel> GetFullPatient(Guid patientId)
        {
            var bearerToken = authDataStorage.GetToken();
            var customerId = CustomerContext.Current.Customer.Id;

            var patient = await patientsService.GetPatientAsync(customerId, patientId, false, bearerToken);

            var fullPatient = Mapper.Map<PatientDto, FullPatientViewModel>(patient);

            var managers = await usersService.GetUsersByIds(patient.CareManagers);

            fullPatient.CareManagers = Mapper.Map<IList<User>, IList<CareManagerViewModel>>(managers);

            return fullPatient;
        }

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreatePatient(CreatePatientRequestDto request)
        {
            request.SiteId = SiteContext.Current.Site.Id;

            var token = authDataStorage.GetToken();
            var response = await patientsService.CreatePatient(CustomerContext.Current.Customer.Id, request, token);

            var createDevice = new CreateDeviceRequestDto()
            {
                PatientId = response.Id,
                BirthDate = request.BirthDate,
                DeviceType = DeviceType.Other
            };

            await devicesService.CreateDevice(CustomerContext.Current.Customer.Id, createDevice, token);

            return response;
        }

        /// <summary>
        /// Updates the patient.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task UpdatePatient(UpdatePatientRequestDto request)
        {
            var customerId = CustomerContext.Current.Customer.Id;
            var authToken = authDataStorage.GetToken();

            await patientsService.UpdatePatient(
                customerId,
                request,
                authToken
                );

            if (request.Status == PatientStatus.Inactive)
            {
                //clear calendar
                await patientsService.ClearCalendar(customerId, request.Id, new ClearCalendarRequestDto()
                    {
                        ScheduledAfter = DateTime.Now,
                        TerminationUtc = DateTime.Now                        
                    }, authToken);
                
                //clear devices
                await devicesService.ClearDevices(customerId, request.Id, authToken);
            }
        }

        /// <summary>
        /// Gets the identifiers within customer scope.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<IdentifierViewModel>> GetIdentifiersWithinCustomerScope()
        {
            var bearerToken = authDataStorage.GetToken();
            var customerId = CustomerContext.Current.Customer.Id;

            var identifiers = await patientsService.GetIdentifiersWithinCustomerScope(customerId, bearerToken);

            return Mapper.Map<IList<IdentifierDto>, IList<IdentifierViewModel>>(identifiers);
        }

        /// <summary>
        /// Suggestions the search.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<IList<SuggestionSearchPatientResultViewModel>> SuggestionSearch(SearchPatientsViewModel searchRequest)
        {
            var bearerToken = authDataStorage.GetToken();
            var searchRequestDto = InitSearchDto(searchRequest);
            var patients = (await patientsService.GetPatients(bearerToken, searchRequestDto)).Results;
         
            return Mapper.Map<List<SuggestionSearchPatientResultViewModel>>(patients);
        }

        /// <summary>
        /// Searches the specified search request.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResult<FullPatientViewModel>> Search(SearchPatientsViewModel searchRequest)
        {
            var bearerToken = authDataStorage.GetToken();
            var searchRequestDto = InitSearchDto(searchRequest);
            var getPatientsResult = await patientsService.GetPatients(bearerToken, searchRequestDto);

            var users = await usersService.GetUsersByIds(getPatientsResult.Results.SelectMany(p => p.CareManagers).ToList());

            var result = new PagedResult<FullPatientViewModel>()
            {
                Results = getPatientsResult
                    .Results
                    .Select(p => 
                    {
                        var mappedPatient = Mapper.Map<FullPatientViewModel>(p);
                                        
                        mappedPatient.CareManagers = Mapper.Map<IList<CareManagerViewModel>>(users.Where(u => p.CareManagers.Contains(u.Id)).ToList());

                        return mappedPatient;
                    })
                    .OrderBy(p => p.FirstName)
                    .ThenBy(p => p.LastName)
                    .ToList(),
                Total = getPatientsResult.Total
            };

            return result;
        }

        /// <summary>
        /// Gets the details of patient which was found.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<SearchPatientDetailsViewModel> GetPatientSearchDetails(Guid patientId)
        {
            var getProgramsResponse = await patientsService.GetCalendarPrograms(CustomerContext.Current.Customer.Id, patientId, null, authDataStorage.GetToken());                        

            var actualPrograms = getProgramsResponse.Results.Where(ActualProgram).OrderBy(p => p.ProgramName);

            var result = new SearchPatientDetailsViewModel()
            {
                Id = patientId,
                Programs = Mapper.Map<List<PatientProgramViewModel>>(actualPrograms)
            };

            return result;
        }

        #region Private methods

        private bool ActualProgram(CalendarProgramResponseDto calendarProgram)
        {
            DateTime startDate = calendarProgram.StartDateUtc;
            DateTime endDate = startDate.AddDays(calendarProgram.EndDay - calendarProgram.StartDay + 1);

            return startDate < DateTime.Now && DateTime.Now < endDate;
        }

        /// <summary>
        /// Initializes dto to find patients in Patients API using model data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private PatientsSearchDto InitSearchDto(SearchPatientsViewModel model)
        {
            var searchRequestDto = Mapper.Map<PatientsSearchDto>(model);
            var authData = this.authDataStorage.GetUserAuthData();

            searchRequestDto.CustomerId = CustomerContext.Current.Customer.Id;
            searchRequestDto.SiteId = SiteContext.Current.Site.Id;            

            if (!model.IncludeInactivePatients)
            {
                searchRequestDto.Statuses = new List<PatientStatus>
                {
                    PatientStatus.Active,
                    PatientStatus.InTraining
                };
            }

            return searchRequestDto;
        }

        /// <summary>
        /// Checks connection.
        /// </summary>
        /// <returns></returns>
        public async Task CheckConnection(Guid patientId)
        {
            var notification = new NotificationDto
            {
                AllTags = true,
                Tags = new List<string>
                {
                    string.Format("maestro-customer-{0}", CustomerContext.Current.Customer.Id),
                    string.Format("maestro-patientid-{0}", patientId)
                },
                Types = new List<RegistrationType>
                {
                    RegistrationType.APN,
                    RegistrationType.GCM
                },
                Data = new
                {
                    PatientDeviceNotification = new
                    {
                        Action = "DeviceSyncRequested",
                        CustomerID = CustomerContext.Current.Customer.Id,
                        PatientID = patientId
                    }
                },
                Sender = string.Format("maestro-customer-{0}", CustomerContext.Current.Customer.Id),
                Message = null
            };

            await messagingHubService.SendPushNotification(notification);
        }

        #endregion
    }
}