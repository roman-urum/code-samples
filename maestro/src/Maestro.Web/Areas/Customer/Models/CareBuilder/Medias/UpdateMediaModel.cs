using System;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Medias
{
    /// <summary>
    /// UpdateMediaModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Areas.Customer.Models.CareBuilder.Medias.MediaViewModel" />
    public class UpdateMediaModel : MediaViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public new Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>
        /// The name of the original file.
        /// </value>
        public string OriginalFileName { get; set; }
    }
}