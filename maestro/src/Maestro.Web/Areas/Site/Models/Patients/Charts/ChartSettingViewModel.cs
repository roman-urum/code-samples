using System.Runtime.Serialization;
using System.Web.Mvc;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Site.Models.ModelBinders;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// Base model for settings of concrete chart.
    /// </summary>
    [ModelBinder(typeof(ChartSettingModelBinder))]
    public abstract class ChartSettingViewModel
    {
        /// <summary>
        /// Type of chart.
        /// </summary>
        public ChartType Type { get; set; }
    }
}