using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ProtocolMapping.
    /// </summary>
    internal class ProtocolMapping : EntityTypeConfiguration<Protocol>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolMapping"/> class.
        /// </summary>
        public ProtocolMapping()
        {
            // Table name
            this.ToTable("Protocols");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            // Many-to-Many /Protocols - Tags/
            this.HasMany(t => t.Tags)
                .WithMany(a => a.Protocols)
                .Map(m =>
                {
                    m.ToTable("ProtocolsToTags");
                    m.MapLeftKey("ProtocolId");
                    m.MapRightKey("TagId");
                });
        }
    }
}