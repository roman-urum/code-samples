using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Dtos
{
    /// <summary>
    /// TextMediaElementSearchDto.
    /// </summary>
    public class TextMediaElementSearchDto : TagsSearchDto
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MediaType? Type { get; set; }
    }
}