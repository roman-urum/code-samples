using System.Threading.Tasks;
using Maestro.Web.Areas.Customer.Models.Settings.General;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ISettingsControllerManager (common things).
    /// </summary>
    public partial interface ISettingsControllerManager
    {
        /// <summary>
        /// Saves customer info and sites in customer service.
        /// Uploads logo.
        /// </summary>
        /// <param name="viewModel">The model.</param>
        /// <returns></returns>
        Task SaveSettings(GeneralSettingsViewModel viewModel);
    }
}