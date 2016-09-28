using System.Linq;
using Maestro.Domain.Constants;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Areas.Customer.Models.Sites;
using Maestro.Web.Security;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    public class SitesControllerManager : ISitesControllerManager
    {
        private readonly IAuthDataStorage authDataStorage;

        public SitesControllerManager(IAuthDataStorage authDataStorage)
        {
            this.authDataStorage = authDataStorage;
        }

        /// <summary>
        /// Generates model for patial view to select required site.
        /// </summary>
        /// <returns></returns>
        public SitesSelectorViewModel InitSitesSelectorModel()
        {
            var userAuthData = this.authDataStorage.GetUserAuthData();
            var availableSites = CustomerContext.Current.Customer.Sites.Where(s => s.Id != SiteContext.Current.Site.Id);

            if (!userAuthData.Role.Equals(Roles.SuperAdmin))
            {
                availableSites = availableSites.Where(s => userAuthData.Sites.Any(sid => s.Id == sid));
            }

            return new SitesSelectorViewModel
            {
                CurrentSiteName = SiteContext.Current.Site.Name,
                AvailableSites = availableSites.ToList()
            };
        }
    }
}