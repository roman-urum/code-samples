namespace CustomerService.Domain.Dtos
{
    /// <summary>
    /// CustomersSearchDto.
    /// </summary>
    public class CustomersSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether [include archived].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include archived]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeArchived { get; set; }
    }
}