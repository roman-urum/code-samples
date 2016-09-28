namespace Maestro.Domain.Enums
{
    using System.ComponentModel;

    public enum DeviceIdType
    {
        [Description("IMEI")]
        IMEI,
        [Description("MAC")]
        MAC,
    }
}