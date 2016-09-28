namespace Maestro.Common
{
    /// <summary>
    /// IBaseAuthTokenStorage.
    /// </summary>
    public interface IBaseAuthTokenStorage
    {
        /// <summary>
        /// Returns authentication token or null if token not saved.
        /// </summary>
        /// <returns></returns>
        string GetToken();
    }
}
