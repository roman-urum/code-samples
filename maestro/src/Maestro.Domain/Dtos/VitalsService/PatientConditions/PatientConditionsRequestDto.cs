using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.PatientConditions
{
    [JsonObject]
    public class PatientConditionsRequestDto
    {
        public IList<Guid> PatientConditionsIds { get; set; }
    }
}
