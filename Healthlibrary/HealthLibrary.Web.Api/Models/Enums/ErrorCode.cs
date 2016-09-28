using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Enums
{
    /// <summary>
    /// ErrorCode.
    /// </summary>
    public enum ErrorCode
    {
        #region [1000-1999]

        #endregion

        #region Client-side error [5000-5999]

        /// <summary>
        /// The invalid access token.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidAccessToken", typeof(GlobalStrings))]
        InvalidAccessToken = 5001,

        /// <summary>
        /// The invalid request
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidRequest", typeof(GlobalStrings))]
        InvalidRequest = 5003,

        #endregion

        #region General errors [6000-6999]

        #endregion

        #region Server-side error [7000-7999]

        /// <summary>
        /// The unspecified server error.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InternalServerError", typeof(GlobalStrings))]
        InternalServerError = 7001

        #endregion
    }
}