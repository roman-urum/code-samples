using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.CareManagers;

using Microsoft.Ajax.Utilities;

namespace Maestro.Web.Areas.Site.Managers.Implementations
{
    /// <summary>
    /// PatientsControllerManager.CareManagers
    /// </summary>
    public partial class PatientsControllerManager
    {
        /// <summary>
        /// Gets the available care managers.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CareManagerViewModel>> GetAvailableCareManagers(SearchCareManagersViewModel searchRequest = null)
        {
            var allCareManagers = await customerUsersService
                .GetCareManagers(
                    CustomerContext.Current.Customer.Id,
                    SiteContext.Current.Site.Id
                );

            if (searchRequest != null
                && searchRequest.OnlyCareManagersWithAssignedPatients.HasValue
                && searchRequest.OnlyCareManagersWithAssignedPatients.Value)
            {
                var bearerToken = authDataStorage.GetToken();

                var patients = await patientsService.GetPatients(bearerToken, new PatientsSearchDto()
                {
                    CustomerId = CustomerContext.Current.Customer.Id,
                    SiteId = SiteContext.Current.Site.Id
                });

                List<CareManagerWithPatientsDetailsViewModel> resultCareManagers = new List<CareManagerWithPatientsDetailsViewModel>();

                foreach (var careManager in allCareManagers)
                {
                    var careManagerPatients = patients.Results.Where(p => p.CareManagers.Contains(careManager.Id));
                    if (careManagerPatients.Any())
                    {
                        var careManagerViewModel = Mapper.Map<CustomerUser, CareManagerWithPatientsDetailsViewModel>(careManager);
                        careManagerViewModel.AssignedPatientStatuses = careManagerPatients.Where(p => p.Status.HasValue).Select(p => p.Status.Value).ToList();

                        resultCareManagers.Add(careManagerViewModel);
                    }
                }


                return resultCareManagers.OfType<CareManagerViewModel>().ToList();
            }

            return Mapper.Map<IList<CustomerUser>, IList<CareManagerViewModel>>(allCareManagers);
        }
    }
}