using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements
{
    /// <summary>
    /// Container to transfer text media object to UI.
    /// </summary>
    public class TextMediaResponseViewModel
    {
        /// <summary>
        /// Id of text element.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of text element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Text of text element.
        /// </summary>
        public LocalizedStringViewModel Text { get; set; }

        /// <summary>
        /// Tags assigned to text element.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Assigned media
        /// </summary>
        public MediaResponseDto Media { get; set; }
    }
}