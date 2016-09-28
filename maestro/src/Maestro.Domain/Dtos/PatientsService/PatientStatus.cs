using System.ComponentModel;

namespace Maestro.Domain.Dtos.PatientsService
{
    /// <summary>
    /// PatientStatus.
    /// </summary>
    public enum PatientStatus
    {
        Inactive = 0,

        Active = 1,

        [Description("In Training")]
        InTraining = 2
    }
}