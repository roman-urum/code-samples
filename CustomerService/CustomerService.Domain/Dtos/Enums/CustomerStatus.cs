using System;
using CustomerService.Common.CustomAttributes;
using CustomerService.Domain.Resources;

namespace CustomerService.Domain.Dtos.Enums
{
    /// <summary>
    /// CustomerStatus.
    /// </summary>
    [Flags]
    public enum CustomerStatus
    {
        Success = 1,

        [DescriptionLocalized("CustomerStatus_NameConflict", typeof(GlobalStrings))]
        NameConflict = 1 << 1,

        [DescriptionLocalized("CustomerStatus_SubdomainConflict", typeof(GlobalStrings))]
        SubdomainConflict = 1 << 2,

        [DescriptionLocalized("CustomerStatus_NotFound", typeof(GlobalStrings))]
        NotFound = 1 << 3
    }
}