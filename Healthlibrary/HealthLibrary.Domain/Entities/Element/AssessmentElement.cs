using System;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// Db entity for assessment elements.
    /// </summary>
    public class AssessmentElement : Element
    {
        /// <summary>
        /// String representation of assessment element type
        /// for storing in db.
        /// </summary>
        public string AssessmentTypeString
        {
            get { return AssessmentType.ToString(); }
            private set { AssessmentType = (AssessmentType) Enum.Parse(typeof (AssessmentType), value, true); }
        }

        /// <summary>
        /// Gets or sets the type of the assessment element.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Name of measurement element.
        /// </summary>
        public string Name { get; set; }
    }
}
