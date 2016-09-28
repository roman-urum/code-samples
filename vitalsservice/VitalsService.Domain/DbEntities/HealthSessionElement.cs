using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// The actual element presented to the patient
    /// </summary>
    public class HealthSessionElement : Entity, IBaseAnalyticsEntity
    {
        /// <summary>
        /// Guid of related health session entity.
        /// </summary>
        public Guid HealthSessionId { get; set; }

        /// <summary>
        /// The UTC date/time this element was answered.
        /// </summary>
        public DateTime? AnsweredUtc { get; set; }

        /// <summary>
        /// Gets or sets the answered timezone.
        /// </summary>
        /// <value>
        /// The answered timezone.
        /// </value>
        public string AnsweredTz { get; set; }

        /// <summary>
        /// Guid of element from health library (text, question, etc).
        /// </summary>
        public Guid ElementId { get; set; }

        /// <summary>
        /// Value or the element (question, text, etc)
        /// </summary>
        [MaxLength(DbConstraints.MaxLength.HealthSessionElementText)]
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
        [MaxLength(DbConstraints.MaxLength.InternalId)]
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
        /// </summary>
        [MaxLength(DbConstraints.MaxLength.ExternalId)]
        public string ExternalId { get; set; }

        /// <summary>
        /// Answer for this element.
        /// </summary>
        public virtual ICollection<HealthSessionElementValue> Values { get; set; }

        /// <summary>
        /// Reference to related health session.
        /// </summary>
        public virtual HealthSession HealthSession { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public virtual ICollection<Note> Notes { get; set; }

        /// <summary>
        /// Gets or sets the health session element alert details.
        /// </summary>
        /// <value>
        /// The health session element alert details.
        /// </value>
        public virtual HealthSessionElementAlert HealthSessionElementAlert { get; set; }

        // ToDo: Potentially will be added according to the requirements
        //public virtual InsightAlert InsightAlert { get; set; }
    }
}
