using DeviceService.Common.CustomAttributes;
using DeviceService.Web.Api.Resources;

namespace DeviceService.Web.Api.Models.Dtos.Enums
{
    /// <summary>
    /// ErrorCode.
    /// </summary>
    public enum ErrorCode
    {
        #region Success [1000-9999]

        #endregion

        #region Client-side errors [5000-5999]

        /// <summary>
        /// The invalid access token.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidAccessToken", typeof(GlobalStrings))]
        InvalidAccessToken = 5001,

        /// <summary>
        /// The invalid request.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidRequest", typeof(GlobalStrings))]
        InvalidRequest = 5002,

        #endregion

        #region General errors [6000-6999]

        #endregion

        #region Server-side errors [7000-7999]

        /// <summary>
        /// The unspecified server error.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InternalServerError", typeof(GlobalStrings))]
        InternalServerError = 7001

        #endregion
    }
}