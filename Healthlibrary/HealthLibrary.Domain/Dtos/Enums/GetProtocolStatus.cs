using HealthLibrary.Common.CustomAttributes;
using HealthLibrary.Domain.Resources;

namespace HealthLibrary.Domain.Dtos.Enums
{
    /// <summary>
    /// GetProtocolStatus.
    /// </summary>
    public enum GetProtocolStatus
    {
        [DescriptionLocalized("ProtocolResult_Success", typeof(GlobalStrings))]
        Success = 1,

        [DescriptionLocalized("ProtocolResult_ProtocolWithSuchIdDoesNotExist", typeof(GlobalStrings))]
        NotFound = 2
    }
}