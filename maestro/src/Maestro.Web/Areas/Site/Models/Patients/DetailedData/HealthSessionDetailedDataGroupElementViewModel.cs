using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.DetailedData
{
    /// <summary>
    /// HealthSessionDetailedDataGroupElementViewModel.
    /// </summary>
    public class HealthSessionDetailedDataGroupElementViewModel
    {
        /// <summary>
        /// The element's identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the answered.
        /// </summary>
        /// <value>
        /// The answered.
        /// </value>
        public DateTimeOffset? Answered { get; set; }

        /// <summary>
        /// Value or the element (question, text, etc)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Type of element.
        /// </summary>
        public HealthSessionElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>
        /// The alert.
        /// </value>
        public AlertViewModel HealthSessionElementAlert { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IList<HealthSessionElementValueViewModel> Values { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        public MediaType? MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media identifier.
        /// </summary>
        public Guid? MediaId { get; set; }

        /// <summary>
        /// Gets or set the media name.
        /// </summary>
        public string MediaName { get; set; }
    }
}