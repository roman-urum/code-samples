using System.Web.Http;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Controllers
{
    /// <summary>
    /// ServiceController.
    /// </summary>
    public abstract class ServiceController : ApiController
    {
        protected const int MAX_PAGE_SIZE = 25;
    }
}