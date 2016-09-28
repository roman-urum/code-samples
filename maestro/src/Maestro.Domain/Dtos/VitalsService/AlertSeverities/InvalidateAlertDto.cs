using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.AlertSeverities
{
    [JsonObject]
    public class InvalidateAlertDto
    {
        public bool IsInvalidated { get; set; }
    }
}