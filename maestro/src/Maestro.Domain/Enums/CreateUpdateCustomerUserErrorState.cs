using System;
using Maestro.Common.CustomAttributes;
using Maestro.Domain.Resources;

namespace Maestro.Domain.Enums
{
    /// <summary>
    /// CreateUpdateCustomerUserErrorState.
    /// </summary>
    [Flags]
    public enum CreateUpdateCustomerUserErrorState
    {
        None = 1 << 1,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_EmailAlreadyExists", typeof(GlobalStrings))]
        EmailAlreadyExists = 1 << 2,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_CustomerDoesNotExist", typeof(GlobalStrings))]
        CustomerDoesNotExist = 1 << 3,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_CustomerUserRoleDoesNotExistWithinCustomer", typeof(GlobalStrings))]
        CustomerUserRoleDoesNotExistWithinCustomer = 1 << 4,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_SitesDoNotExistWithinCustomer", typeof(GlobalStrings))]
        SitesDoNotExistWithinCustomer = 1 << 5,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_CustomerUserIdAlreadyExists", typeof(GlobalStrings))]
        CustomerUserIdAlreadyExists = 1 << 6,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_NPIAlreadyExists", typeof(GlobalStrings))]
        NPIAlreadyExists = 1 << 7,

        [DescriptionLocalized("CreateUpdateCustomerUserErrorState_CustomerUserDoesNotExist", typeof(GlobalStrings))]
        CustomerUserDoesNotExist = 1 << 8
    }
}