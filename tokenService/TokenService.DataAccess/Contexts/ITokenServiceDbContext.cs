using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Contexts
{
    public interface ITokenServiceDbContext
    {
        DbSet<Credential> Credentials { get; set; }

        DbSet<Group> Groups { get; set; }

        DbSet<Policy> Policies { get; set; }

        DbSet<Principal> Principals { get; set; }

        DbSet<DeviceCertificate> DevicesCertificates { get; set; }
    }
}
