using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// HealthSessionElementValueViewModel.
    /// </summary>
    public abstract class HealthSessionElementValueViewModel
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public HealthSessionElementValueType Type { get; set; }
    }
}