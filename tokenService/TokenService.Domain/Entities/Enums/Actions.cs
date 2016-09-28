using System;
using System.Runtime.Serialization;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums
{
    /// <summary>
    /// Actions.
    /// </summary>
    [Flags]
    public enum Actions
    {
        [EnumMember(Value = "Delete")]
        Delete = 0x01,
        [EnumMember(Value = "Get")]
        Get = 0x02,
        [EnumMember(Value = "Head")]
        Head = 0x04,
        [EnumMember(Value = "Options")]
        Options = 0x08,
        [EnumMember(Value = "Patch")]
        Patch = 0x10,
        [EnumMember(Value = "Post")]
        Post = 0x20,
        [EnumMember(Value = "Put")]
        Put = 0x40,
        [EnumMember(Value = "Any")]
        Any = Delete | Get | Head | Options | Patch | Post | Put
    }
}