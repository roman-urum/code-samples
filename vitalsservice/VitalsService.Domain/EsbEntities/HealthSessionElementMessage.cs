using System;
using System.Collections.ObjectModel;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.EsbEntities
{
    /// <summary>
    /// HealthSessionElementMessage.
    /// </summary>
    public class HealthSessionElementMessage
    {
        /// <summary>
        /// the UTC date/time this element was answered.
        /// </summary>
        public DateTime AnsweredUtc { get; set; }

        /// <summary>
        /// Guid of element from health library (text, question, etc).
        /// </summary>
        public Guid ElementId { get; set; }

        /// <summary>
        /// Guid of related health session entity.
        /// </summary>
        public Guid HealthSessionId { get; set; }

        /// <summary>
        /// Value or the element (question, text, etc)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Type of element.
        /// </summary>
        public HealthSessionElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the internal question id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Answer for this element.
        /// </summary>
        public virtual Collection<HealthSessionElementValueMessage> Values { get; set; }
    }
}