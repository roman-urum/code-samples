using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    public class GroupModel
    {
        [MaxLength(DbConstraints.MaxLength.GroupName)]
        public string Name { get; set; }

        [MaxLength(DbConstraints.MaxLength.GroupDescription)]
        public string Description { get; set; }

        public bool Disabled { get; set; }

        public List<PolicyModel> Policies { get; set; }

    }
}