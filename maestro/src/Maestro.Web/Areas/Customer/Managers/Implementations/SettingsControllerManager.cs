using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Areas.Customer.Models.Settings.General;
using Maestro.Web.Security;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// SettingsControllerManager.
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Managers.Interfaces.ISettingsControllerManager" />
    public partial class SettingsControllerManager : ISettingsControllerManager
    {
        private readonly ICustomersService customersService;
        private readonly IAuthDataStorage authDataStorage;
        private readonly IVitalsService vitalsService;
        private readonly ICustomerUsersService customerUsersService;
        private readonly IPatientsService patientsService;
        private readonly IHealthLibraryService healthLibraryService;
        private readonly ICustomerContext customerContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsControllerManager" /> class.
        /// </summary>
        /// <param name="customersService">The customers service.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        /// <param name="vitalsService">The vitals service.</param>
        /// <param name="customerUsersService">The customer users service.</param>
        /// <param name="patientsService">The patients service.</param>
        /// <param name="customerContext">The customer context.</param>
        public SettingsControllerManager(
            ICustomersService customersService,
            IAuthDataStorage authDataStorage,
            IVitalsService vitalsService,
            ICustomerUsersService customerUsersService,
            IPatientsService patientsService,
            ICustomerContext customerContext,
            IHealthLibraryService healthLibraryService
        )
        {
            this.customersService = customersService;
            this.authDataStorage = authDataStorage;
            this.vitalsService = vitalsService;
            this.customerUsersService = customerUsersService;
            this.patientsService = patientsService;
            this.customerContext = customerContext;
            this.healthLibraryService = healthLibraryService;
        }

        /// <summary>
        /// Saves customer info and sites in customer service.
        /// Uploads logo.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task SaveSettings(GeneralSettingsViewModel viewModel)
        {
            if (viewModel.LogoImage != null)
            {
                viewModel.LogoPath = await SaveLogoImage(viewModel.LogoImage);
            }

            var token = authDataStorage.GetToken();
            var request = Mapper.Map<GeneralSettingsViewModel, UpdateCustomerRequestDto>(viewModel);
            request.Id = customerContext.Customer.Id;

            // Updating customer
            await customersService.UpdateCustomer(request, token);
        }
    }
}