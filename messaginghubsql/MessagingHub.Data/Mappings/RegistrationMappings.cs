using System.Data.Entity.ModelConfiguration;
using MessagingHub.Data.Models;

namespace MessagingHub.Data.Mappings
{
    internal class RegistrationMappings: EntityTypeConfiguration<Registration>
    {
        public RegistrationMappings()
        {
            this.ToTable("Registrations");

            // One-to-Many /Application - Registrations/
            this.HasOptional(r => r.Application)
                .WithMany(a => a.Registrations)
                .HasForeignKey(r => r.ApplicationId)
                .WillCascadeOnDelete(false);
        }
    }
}
