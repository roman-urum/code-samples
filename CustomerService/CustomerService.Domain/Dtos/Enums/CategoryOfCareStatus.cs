using System;
using CustomerService.Common.CustomAttributes;
using CustomerService.Domain.Resources;

namespace CustomerService.Domain.Dtos.Enums
{
    /// <summary>
    /// CategoryOfCareStatus enum.
    /// </summary>
    [Flags]
    public enum CategoryOfCareStatus
    {
        Success = 1,

        [DescriptionLocalized("CategoryOfCareStatus_NameConflict", typeof(GlobalStrings))]
        NameConflict = 1 << 1,

        [DescriptionLocalized("CategoryOfCareStatus_CustomerDoesNotExist", typeof(GlobalStrings))]
        CustomerDoesNotExist = 1 << 2,

        [DescriptionLocalized("CategoryOfCareStatus_CategoryOfCareWithSuchIdDoesNotExist", typeof(GlobalStrings))]
        CategoryOfCareWithSuchIdDoesNotExist = 1 << 3
    }
}