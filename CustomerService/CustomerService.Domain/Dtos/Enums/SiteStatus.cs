using System;
using CustomerService.Common.CustomAttributes;
using CustomerService.Domain.Resources;

namespace CustomerService.Domain.Dtos.Enums
{
    /// <summary>
    /// SiteStatus.
    /// </summary>
    [Flags]
    public enum SiteStatus
    {
        Success = 1,

        [DescriptionLocalized("SiteStatus_SiteAlreadyExists", typeof(GlobalStrings))]
        NameConflict = 1 << 1,

        [DescriptionLocalized("SiteStatus_SiteWithSuchNpiAlreadyExistsForCurrentCustomer", typeof(GlobalStrings))]
        NPIConflict = 1 << 2,

        [DescriptionLocalized("SiteStatus_SiteWithSuchCustomerSiteIdAlreadyExistsForCurrentCustomer", typeof(GlobalStrings))]
        CustomerSiteIdConflict = 1 << 3,

        [DescriptionLocalized("SiteStatus_CategoryOfCareConflict", typeof(GlobalStrings))]
        CategoryOfCareConflict = 1 << 4,

        [DescriptionLocalized("CustomerStatus_NotFound", typeof(GlobalStrings))]
        CustomerNotFound = 1 << 5,

        [DescriptionLocalized("SiteStatus_SiteDoesNotExists", typeof(GlobalStrings))]
        NotFound = 1 << 6,

        [DescriptionLocalized("CustomerStatus_OrganizationNotFound", typeof(GlobalStrings))]
        OrganizationNotFound = 1 << 7
    }
}