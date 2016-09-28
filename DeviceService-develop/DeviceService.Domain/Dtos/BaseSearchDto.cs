namespace DeviceService.Domain.Dtos
{
    /// <summary>
    /// BaseSearchDto.
    /// </summary>
    public class BaseSearchDto
    {
        private const int DefaultSkip = 0;
        private const int DefaultTake = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchDto"/> class.
        /// </summary>
        public BaseSearchDto()
        {
            Skip = DefaultSkip;
            Take = DefaultTake;
        }

        /// <summary>
        /// Generic text search.
        /// </summary>
        /// <value>
        /// The q.
        /// </value>
        public string Q { get; set; }

        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        /// <value>
        /// The take.
        /// </value>
        public int Take { get; set; }
    }
}