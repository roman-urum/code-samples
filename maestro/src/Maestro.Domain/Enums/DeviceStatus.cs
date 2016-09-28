using System.ComponentModel;

namespace Maestro.Domain.Enums
{
    public enum DeviceStatus
    {
        [Description("Not Activated")]
        NotActivated,

        [Description("Activated")]
        Activated,

        [Description("Decommission Requested")]
        DecommissionRequested,

        [Description("Decommission Acknowledged")]
        DecommissionAcknowledged,

        [Description("Decommission Started")]
        DecommissionStarted,

        [Description("Decommission Completed")]
        DecommissionCompleted
    }
}