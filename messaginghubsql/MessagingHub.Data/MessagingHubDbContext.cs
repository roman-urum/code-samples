using MessagingHub.Data.Models;
using System.Data.Entity;

namespace MessagingHub.Data
{
    public class MessagingHubDbContext : DbContext
    {
        public MessagingHubDbContext()
            : base("name=MessagingHubDbContext")
        { }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<RegistrationTag> Tags { get; set; }

        public DbSet<Application> Applications { get; set; }
    }
}
