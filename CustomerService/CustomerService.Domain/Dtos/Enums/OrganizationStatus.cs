using System;
using CustomerService.Common.CustomAttributes;
using CustomerService.Domain.Resources;

namespace CustomerService.Domain.Dtos.Enums
{
    /// <summary>
    /// OrganizationStatus.
    /// </summary>
    [Flags]
    public enum OrganizationStatus
    {
        Success = 1 << 1,

        [DescriptionLocalized("OrganizationStatus_NameConflict", typeof(GlobalStrings))]
        NameConflict = 1 << 2,

        [DescriptionLocalized("OrganizationStatus_NotFound", typeof(GlobalStrings))]
        NotFound = 1 << 3,

        [DescriptionLocalized("OrganizationStatus_ParentNotFound", typeof(GlobalStrings))]
        ParentNotFound = 1 << 4
    }
}