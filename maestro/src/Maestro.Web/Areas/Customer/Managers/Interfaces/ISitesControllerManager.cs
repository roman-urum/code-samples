using Maestro.Web.Areas.Customer.Models.Sites;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    public interface ISitesControllerManager
    {
        /// <summary>
        /// Generates model for patial view to select required site.
        /// </summary>
        /// <returns></returns>
        SitesSelectorViewModel InitSitesSelectorModel();
    }
}
