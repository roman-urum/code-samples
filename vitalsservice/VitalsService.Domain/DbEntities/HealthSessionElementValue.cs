using System;
using System.ComponentModel.DataAnnotations.Schema;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Base info about answer for health element.
    /// </summary>
    public abstract class HealthSessionElementValue : Entity
    {
        /// <summary>
        /// Gets or sets the health session element identifier.
        /// </summary>
        /// <value>
        /// The health session element identifier.
        /// </value>
        [ForeignKey("HealthSessionElement")]
        public Guid HealthSessionElementId { get; set; }

        /// <summary>
        /// Type of answer.
        /// </summary>
        public HealthSessionElementValueType Type { get; set; }

        /// <summary>
        /// Reference to health session element for which answer is created.
        /// </summary>
        public virtual HealthSessionElement HealthSessionElement { get; set; }
    }
}