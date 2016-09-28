namespace Maestro.Domain.Dtos
{
    /// <summary>
    /// PostResponseDto.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostResponseDto<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public T Id { get; set; }
    }
}