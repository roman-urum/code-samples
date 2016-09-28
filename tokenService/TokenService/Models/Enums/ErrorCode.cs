using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CareInnovations.HealthHarmony.Maestro.TokenService.Common.Attributes;
using CareInnovations.HealthHarmony.Maestro.TokenService.Resources;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models.Enums
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
        /// The invalid request
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidRequest", typeof(GlobalStrings))]
        InvalidRequest = 5003,

        /// <summary>
        /// Credentials used for authorization is expired.
        /// </summary>
        [DescriptionLocalized("ErrorCode_CredentialsExpired", typeof(GlobalStrings))]
        CredentialsExpired = 5004,

        /// <summary>
        /// Invalid credential value is provided.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidCredentialValue", typeof(GlobalStrings))]
        InvalidCredentialValue = 5005,

        /// <summary>
        /// Identifies that the same credential was used earlier.
        /// </summary>
        [DescriptionLocalized("ErrorCode_CredentialAlreadyUsed", typeof(GlobalStrings))]
        CredentialAlreadyUsed = 5006,

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