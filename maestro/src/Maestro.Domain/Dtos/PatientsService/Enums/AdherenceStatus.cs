namespace Maestro.Domain.Dtos.PatientsService.Enums
{
    /// <summary>
    /// AdherenceStatus.
    /// </summary>
    public enum AdherenceStatus
    {
        Scheduled = 1,
        Started,
        PartiallyCompleted,
        Completed,
        Missed
    }
}