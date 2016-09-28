using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;
using Isg.EntityFramework;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Contexts
{
    public class TokenServiceDbContext : DbContextBase, ITokenServiceDbContext
    {
        public TokenServiceDbContext() : 
            base("TokenServiceDbContext")
        {}

        #region DbSets

        public DbSet<Credential> Credentials { get; set; }

        public DbSet<Group> Groups { get; set; }
        
        public DbSet<Policy> Policies { get; set; }

        public DbSet<Principal> Principals { get; set; }

        public DbSet<DeviceCertificate> DevicesCertificates { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

#if DEBUG
            LogGeneratedSql();
#endif
            base.OnModelCreating(modelBuilder);
        }

        private void LogGeneratedSql()
        {
            Database.Log = log => Debug.Write(log);
        }
    }
}
