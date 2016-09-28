using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// NoteStatus.
    /// </summary>
    [Flags]
    public enum NoteStatus
    {
        Success = 1 << 1,

        [DescriptionLocalized("NoteStatus_MeasurementConflict", typeof(GlobalStrings))]
        MeasurementConflict = 1 << 2,

        [DescriptionLocalized("NoteStatus_HealthSessionElementConflict", typeof(GlobalStrings))]
        HealthSessionElementConflict = 1 << 3,

        [DescriptionLocalized("NoteStatus_NotFound", typeof(GlobalStrings))]
        NotFound = 1 << 4
    }
}