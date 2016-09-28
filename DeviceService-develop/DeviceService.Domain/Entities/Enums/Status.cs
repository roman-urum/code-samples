namespace DeviceService.Domain.Entities.Enums
{
    public enum Status
    {
        NotActivated,
        Activated,
        DecommissionRequested,
        DecommissionAcknowledged,
        DecommissionStarted,
        DecommissionCompleted
    }
}