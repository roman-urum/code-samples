using System;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements
{
    /// <summary>
    /// UpdateTextMediaElementViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements.BaseTextMediaElementViewModel" />
    public class UpdateTextMediaElementViewModel : BaseTextMediaElementViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        public UpdateMediaModel Media { get; set; }
    }
}