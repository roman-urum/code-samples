using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maestro.Domain.Dtos.PatientsService.Enums
{
    /// <summary>
    /// WeekDays.
    /// </summary>
    public enum WeekDays
    {
        None = 0,
        SU = 1 << 1,
        MO = 1 << 2,
        TU = 1 << 3,
        WE = 1 << 4,
        TH = 1 << 5,
        FR = 1 << 6,
        SA = 1 << 7
    }
}
