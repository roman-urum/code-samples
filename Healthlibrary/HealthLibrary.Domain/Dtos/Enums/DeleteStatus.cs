using System.ComponentModel;

namespace HealthLibrary.Domain.Dtos.Enums
{
    /// <summary>
    /// Enum for status of delete transaction.
    /// </summary>
    public enum DeleteStatus
    {
        Success = 1,
        [Description("{0} was not found")]
        NotFound = 2,
        [Description("{0} is in use")]
        InUse = 3
    }
}