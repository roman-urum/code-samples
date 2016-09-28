using System.ComponentModel;
using Maestro.Web.Filters;

namespace Maestro.Web.Models.Api.Dtos.Enums
{
    /// <summary>
    /// ErrorCode.
    /// </summary>
    public enum ErrorCode
    {
        #region Client-side errors

        /// <summary>
        /// The customer user with such customer user identifier does not exist
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserWithSuchCustomerUserIdDoesNotExist")]
        CustomerUserWithSuchCustomerUserIdDoesNotExist = 1,

        /// <summary>
        /// The customer user with such npi does not exist
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserWithSuchNPIDoesNotExist")]
        CustomerUserWithSuchNPIDoesNotExist = 2,

        /// <summary>
        /// The invalid request.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidRequest")]
        InvalidRequest = 3,

        /// <summary>
        /// The customer does not exist.
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerDoesNotExist")]
        CustomerDoesNotExist = 4,

        /// <summary>
        /// The customer role does not exist within customer.
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserRoleDoesNotExistWithinCustomer")]
        CustomerUserRoleDoesNotExistWithinCustomer = 5,

        /// <summary>
        /// The sites do not exist within customer
        /// </summary>
        [DescriptionLocalized("ErrorCode_SitesDoNotExistWithinCustomer")]
        SitesDoNotExistWithinCustomer = 6,

        /// <summary>
        /// The invalid access token.
        /// </summary>
        [DescriptionLocalized("ErrorCode_ApiAccessTokenInvalid")]
        InvalidAccessToken = 7,

        /// <summary>
        /// The token not provided
        /// </summary>
        [DescriptionLocalized("ErrorCode_TokenNotProvided")]
        TokenNotProvided = 8,

        /// <summary>
        /// The customer user does not exist
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserDoesNotExist")]
        CustomerUserDoesNotExist = 9,

        /// <summary>
        /// The customer user with such email already exists
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserWithSuchEmailAlreadyExists")]
        CustomerUserWithSuchEmailAlreadyExists = 10,

        /// <summary>
        /// The customer user with such customer user identifier already exists
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserWithSuchCustomerUserIdAlreadyExists")]
        CustomerUserWithSuchCustomerUserIdAlreadyExists = 11,

        /// <summary>
        /// The customer user with such npi already exists
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerUserWithSuchNPIAlreadyExists")]
        CustomerUserWithSuchNPIAlreadyExists = 12,

        #endregion

        #region Server-side errors

        /// <summary>
        /// The unspecified server error.
        /// </summary>
        [Description("Unspecified server error")]
        InternalServerError = 101
        
        #endregion
    }
}