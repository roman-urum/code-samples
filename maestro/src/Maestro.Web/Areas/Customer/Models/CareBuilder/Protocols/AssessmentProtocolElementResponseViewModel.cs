using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    /// <summary>
    /// AssessmentProtocolElementResponseViewModel.
    /// </summary>
    public class AssessmentProtocolElementResponseViewModel : ElementResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the Assessment.
        /// </summary>
        /// <value>
        /// The type of the Assessment.
        /// </value>
        public AssessmentType AssessmentType { get; set; }
    }
}