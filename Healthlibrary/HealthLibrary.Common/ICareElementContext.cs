using System.Threading.Tasks;

namespace HealthLibrary.Common
{
    /// <summary>
    /// Context of customer which uses database.
    /// </summary>
    public interface ICareElementContext
    {
        /// <summary>
        /// Returns id of current customer.
        /// Returns null if application used context of care innovations generic content.
        /// </summary>
        int CustomerId { get; }

        /// <summary>
        /// Returns language which should be used in context of current request.
        /// </summary>
        string Language { get; }

        /// <summary>
        /// Returns language which should be used by default.
        /// </summary>
        string DefaultLanguage { get; }

        /// <summary>
        /// Returns container name for blob storage
        /// based on content type (customer or generic).
        /// </summary>
        /// <returns></returns>
        string GetMediaContainerName();

        /// <summary>
        /// Verifies if user who send request is care innovations admin.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCIUser();
    }
}
