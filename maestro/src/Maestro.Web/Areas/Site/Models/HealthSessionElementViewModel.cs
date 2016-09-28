using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// HealthSessionElementViewModel.
    /// </summary>
    public class HealthSessionElementViewModel
    {
        /// <summary>
        /// The element's identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The UTC date/time this element was answered.
        /// </summary>
        public DateTimeOffset? Answered { get; set; }

        /// <summary>
        /// Guid of element from health library (text, question, etc).
        /// </summary>
        public Guid ElementId { get; set; }

        /// <summary>
        /// Value or the element (question, text, etc)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Type of element.
        /// </summary>
        public HealthSessionElementType Type { get; set; }

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

        /// <summary>
        /// Gets or sets the internal question id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the health session element alert.
        /// </summary>
        /// <value>
        /// The health session element alert.
        /// </value>
        public HealthSessionElementAlertResponseDto HealthSessionElementAlert { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IList<HealthSessionElementValueViewModel> Values { get; set; }
    }
}