using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ProtocolStringMapping.
    /// </summary>
    internal class ProtocolStringMapping : EntityTypeConfiguration<ProtocolString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolStringMapping"/> class.
        /// </summary>
        public ProtocolStringMapping()
        {
            // Table name
            this.ToTable("ProtocolStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /Protocol - ProtocolStrings/
            this.HasRequired(t => t.Protocol)
                .WithMany(a => a.NameLocalizedStrings)
                .HasForeignKey(t => t.ProtocolId)
                .WillCascadeOnDelete(true);
        }
    }
}