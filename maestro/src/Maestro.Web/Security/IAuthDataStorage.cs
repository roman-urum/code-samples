using Maestro.Common;

namespace Maestro.Web.Security
{
    /// <summary>
    /// Declares methods to store auth token.
    /// </summary>
    public interface IAuthDataStorage : IBaseAuthTokenStorage
    {
        /// <summary>
        /// Saves API authentication token and user data.
        /// </summary>
        /// <param name="authData"></param>
        void Save(UserAuthData authData);

        /// <summary>
        /// Clears saved authentication data.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns saved authentication info.
        /// </summary>
        UserAuthData GetUserAuthData();
    }
}
