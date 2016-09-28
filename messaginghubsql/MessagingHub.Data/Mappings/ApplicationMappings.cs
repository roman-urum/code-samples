using System.Data.Entity.ModelConfiguration;

using MessagingHub.Data.Models;

namespace MessagingHub.Data.Mappings
{
    internal class ApplicationMappings: EntityTypeConfiguration<Application>
    {
        public ApplicationMappings()
        {
            this.ToTable("Applications");            
        }
    }
}
