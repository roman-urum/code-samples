namespace VitalsService.Web.Api.Models
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PostResponseDto{T}"/> class.
        /// </summary>
        public PostResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostResponseDto{T}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PostResponseDto(T id)
        {
            this.Id = id;
        }
    }
}