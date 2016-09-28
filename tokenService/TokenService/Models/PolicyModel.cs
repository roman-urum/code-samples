using System.ComponentModel.DataAnnotations;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    public class PolicyModel
    {
        [MaxLength(DbConstraints.MaxLength.PolicyName)]
        public string Name { get; set; }

        public string Effect { get; set; }

        [MaxLength(DbConstraints.MaxLength.ServiceName)]
        public string Service { get; set; }

        [MaxLength(DbConstraints.MaxLength.ControllerName)]
        public string Controller { get; set; }

        public string Action { get; set; }

        public int? Customer { get; set; }
    }
}