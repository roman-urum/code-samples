using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Protocol;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ProtocolElementMapping.
    /// </summary>
    internal class ProtocolElementMapping : EntityTypeConfiguration<ProtocolElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolMapping"/> class.
        /// </summary>
        public ProtocolElementMapping()
        {
            // Table name
            this.ToTable("ProtocolElements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /Protocol - ProtocolElements/
            this.HasRequired(t => t.Protocol)
                .WithMany(a => a.ProtocolElements)
                .HasForeignKey(t => t.ProtocolId)
                .WillCascadeOnDelete(true);

            // One-to-Many /ProtocolElement - ProtocolElements/
            this.HasOptional(t => t.NextProtocolElement)
                .WithMany()
                .HasForeignKey(t => t.NextProtocolElementId)
                .WillCascadeOnDelete(false);

            // One-to-Many /Element - ProtocolElements/
            this.HasRequired(t => t.Element)
                .WithMany(t => t.ProtocolElements)
                .HasForeignKey(t => t.ElementId)
                .WillCascadeOnDelete(true);
        }
    }
}