namespace CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results
{
    /// <summary>
    /// Action result with generic status.
    /// </summary>
    /// <typeparam name="TStatus"></typeparam>
    /// <typeparam name="TContent"></typeparam>
    public class ServiceActionResult<TStatus, TContent>
    {
        public TStatus Status { get; set; }

        public TContent Content { get; set; }

        public ServiceActionResult(TStatus status)
        {
            this.Status = status;
        }

        public ServiceActionResult(TStatus status, TContent content)
            : this(status)
        {
            this.Content = content;
        }

        /// <summary>
        /// Creates clone of current entity with the same status.
        /// </summary>
        /// <typeparam name="TClone"></typeparam>
        /// <returns></returns>
        public ServiceActionResult<TStatus, TClone> Clone<TClone>()
        {
            return new ServiceActionResult<TStatus, TClone>(this.Status);
        }

        /// <summary>
        /// Creates clone of current entity with the same status and new provided content.
        /// </summary>
        /// <typeparam name="TClone"></typeparam>
        /// <returns></returns>
        public ServiceActionResult<TStatus, TClone> Clone<TClone>(TClone content)
        {
            return new ServiceActionResult<TStatus, TClone>(this.Status, content);
        }
    }
}
