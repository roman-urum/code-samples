using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements
{
    /// <summary>
    /// CreateTextMediaElementViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements.BaseTextMediaElementViewModel" />
    public class CreateTextMediaElementViewModel : BaseTextMediaElementViewModel
    {
        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        public MediaViewModel Media { get; set; }
    }
}