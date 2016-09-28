using System;

namespace CustomerService.Web.Api.Models.Dtos.CategoryOfCare
{
    /// <summary>
    /// CategoryOfCareResponseDto.
    /// </summary>
    public class CategoryOfCareResponseDto : CategoryOfCareRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}