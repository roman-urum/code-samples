using System;
using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    /// <summary>
    /// CreateUpdateProtocolStatus.
    /// </summary>
    [Flags]
    public enum CreateUpdateProtocolStatus
    {
        [DescriptionLocalized("ProtocolResult_Success", typeof(GlobalStrings))]
        Success = 1 << 1,

        [DescriptionLocalized("ProtocolResult_FirstProtocolElementClientIdInvalid", typeof(GlobalStrings))]
        FirstProtocolElementClientIdInvalid = 1 << 2,

        [DescriptionLocalized("ProtocolResult_OneOfNextProtocolElementClientIdsInProtocolElementsInvalid", typeof(GlobalStrings))]
        OneOfNextProtocolElementClientIdsInProtocolElementsInvalid = 1 << 3,

        [DescriptionLocalized("ProtocolResult_OneOfNextProtocolElementClientIdsInBranchesInvalid", typeof(GlobalStrings))]
        OneOfNextProtocolElementClientIdsInBranchesInvalid = 1 << 4,

        [DescriptionLocalized("ProtocolResult_OneOfElementIdsInvalid", typeof(GlobalStrings))]
        OneOfElementIdsInvalid = 1 << 5,

        [DescriptionLocalized("ProtocolResult_OneOfProtocolElementsIsNotUsedWithinProtocol", typeof(GlobalStrings))]
        OneOfProtocolElementsIsNotUsedWithinProtocol = 1 << 6,

        [DescriptionLocalized("ProtocolResult_OneOfBranchesOrAlertsInproperlyFilledIn", typeof(GlobalStrings))]
        OneOfBranchesOrAlertsInproperlyFilledIn = 1 << 7,
        
        [DescriptionLocalized("ProtocolResult_ProtocolElementsListShouldBeDistinct", typeof(GlobalStrings))]
        ProtocolElementsListShouldBeDistinct = 1 << 8,

        [DescriptionLocalized("ProtocolResult_ProtocolElementShouldNotPointToItself", typeof(GlobalStrings))]
        ProtocolElementShouldNotPointToItself = 1 << 9,

        [DescriptionLocalized("ProtocolResult_ProtocolWithSuchIdDoesNotExist", typeof(GlobalStrings))]
        ProtocolWithSuchIdDoesNotExist = 1 << 10
    }
}