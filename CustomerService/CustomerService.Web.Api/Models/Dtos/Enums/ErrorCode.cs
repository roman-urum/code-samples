using CustomerService.Web.Api.CustomAttributes;

namespace CustomerService.Web.Api.Models.Dtos.Enums
{
    /// <summary>
    /// ErrorCode.
    /// </summary>
    public enum ErrorCode
    {
        #region Success [1000-1999]

        #endregion

        #region Client-side error [5000-5999]

        /// <summary>
        /// The user does not exists.
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerDoesNotExists")]
        CustomerDoesNotExists = 5001,

        /// <summary>
        /// The site does not exists.
        /// </summary>
        [DescriptionLocalized("ErrorCode_SiteDoesNotExists")]
        SiteDoesNotExists = 5002,

        /// <summary>
        /// The no access for site updating.
        /// </summary>
        [DescriptionLocalized("ErrorCode_NoRightsSiteUpdating")]
        ForbiddenForSiteUpdating = 5003,

        /// <summary>
        /// The invalid request
        /// </summary>
        [DescriptionLocalized("ErrorCode_InvalidRequest")]
        InvalidRequest = 5004,

        #endregion

        #region General errors [6000-6999]

        /// <summary>
        /// The invalid access token.
        /// </summary>
        [DescriptionLocalized("ErrorCode_ApiAccessTokenInvalid")]
        InvalidAccessToken = 6011,

        /// <summary>
        /// The token not provided
        /// </summary>
        [DescriptionLocalized("ErrorCode_TokenNotProvided")]
        TokenNotProvided = 6012,

        /// <summary>
        /// The incorrect file.
        /// </summary>
        [DescriptionLocalized("ErrorCode_IncorrectFile")]
        IncorrectFile = 6014,

        /// <summary>
        /// The subdomain is not available
        /// </summary>
        [DescriptionLocalized("ErrorCode_SubdomainIsNotAvailable")]
        SubdomainIsNotAvailable = 6016,

        /// <summary>
        /// The customer name is not available
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerNameAlreadyExists")]
        CustomerNameIsNotAvailable = 6017,

        /// <summary>
        /// The customer must have a site
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerMustHaveASite")]
        CustomerMustHaveASite = 6018,

        /// <summary>
        /// The customer name conflict
        /// </summary>
        [DescriptionLocalized("ErrorCode_CustomerNameConflict")]
        CustomerNameConflict = 6019,

        /// <summary>
        /// The site already exists for current user
        /// </summary>
        [DescriptionLocalized("ErrorCode_SiteAlreadyExists")]
        SiteAlreadyExists = 6020,

        /// <summary>
        /// The site with such NPI already exists for current customer
        /// </summary>
        [DescriptionLocalized("ErrorCode_SiteWithSuchNpiAlreadyExistsForCurrentCustomer")]
        SiteWithSuchNpiAlreadyExistsForCurrentCustomer = 6021,

        /// <summary>
        /// The site with such customer site identifier already exists for current customer
        /// </summary>
        [DescriptionLocalized("ErrorCode_SiteWithSuchCustomerSiteIdAlreadyExistsForCurrentCustomer")]
        SiteWithSuchCustomerSiteIdAlreadyExistsForCurrentCustomer = 6022,

        #endregion

        #region Server-side error [7000-7999]

        /// <summary>
        /// The unspecified server error.
        /// </summary>
        [DescriptionLocalized("ErrorCode_InternalServerError")]
        InternalServerError = 7001

        #endregion
    }
}