namespace CustomerService.Web.Api.Models.Dtos
{
    /// <summary>
    /// Base model for response of request
    /// to create new entity.
    /// </summary>
    public class PostResponseDto<T>
    {
        /// <summary>
        /// Id of new entity.
        /// </summary>
        public T Id { get; set; }
    }
}