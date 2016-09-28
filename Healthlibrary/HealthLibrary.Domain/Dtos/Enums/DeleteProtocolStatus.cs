using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    /// <summary>
    /// DeleteProtocolStatus.
    /// </summary>
    public enum DeleteProtocolStatus
    {
        [DescriptionLocalized("ProtocolResult_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("ProtocolResult_ProtocolWithSuchIdDoesNotExist", typeof(GlobalStrings))]
        NotFound = 2,

        [DescriptionLocalized("ProtocolResult_ProtocolAlreadyInUse", typeof(GlobalStrings))]
        InUse = 3
    }
}