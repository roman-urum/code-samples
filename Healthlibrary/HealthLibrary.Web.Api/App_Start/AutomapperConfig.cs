using HealthLibrary.Common;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api
{
    /// <summary>
    /// Provides methods to initialize mapping rules.
    /// </summary>
    public static class AutomapperConfig
    {
        /// <summary>
        /// Initializes mapping rules.
        /// </summary>
        public static void RegisterRules()
        {
            var mappings = ServiceLocator.Current.GetAllInstances<IClassMapping>();

            foreach (var mapping in mappings)
            {
                mapping.CreateMap();
            }
        }
    }
}