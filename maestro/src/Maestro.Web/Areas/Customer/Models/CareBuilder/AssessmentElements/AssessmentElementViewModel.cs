using System;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.AssessmentElements
{
    /// <summary>
    /// Model to return assessment data to UI.
    /// </summary>
    public class AssessmentElementViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the assessment.
        /// </summary>
        /// <value>
        /// The type of the assessment.
        /// </value>
        public AssessmentType AssessmentType { get; set; }
    }
}