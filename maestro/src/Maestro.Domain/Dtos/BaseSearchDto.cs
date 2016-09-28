using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos
{
    /// <summary>
    /// BaseSearchDto.
    /// </summary>
    public class BaseSearchDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchDto"/> class.
        /// </summary>
        public BaseSearchDto()
        {
            Skip = 0;
            Take = int.MaxValue;
        }

        /// <summary>
        /// Generic text search.
        /// </summary>
        /// <value>
        /// The q.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public string Q { get; set; }

        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        /// <value>
        /// The take.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int Take { get; set; }
    }
}