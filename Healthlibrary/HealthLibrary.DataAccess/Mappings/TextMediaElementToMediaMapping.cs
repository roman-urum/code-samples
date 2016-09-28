using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// TextMediaElementToMediaMapping.
    /// </summary>
    internal class TextMediaElementToMediaMapping : EntityTypeConfiguration<TextMediaElementToMedia>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMediaElementToMediaMapping"/> class.
        /// </summary>
        public TextMediaElementToMediaMapping()
        {
            // Table name
            this.ToTable("TextMediaElementsToMedias");

            // Primary Key
            this.HasKey(e => new { e.TextMediaElementId, e.MediaId });

            // Properties
            this.Property(e => e.Language)
                .IsRequired()
                .HasMaxLength(5);

            // One-to-Many /TextMediaElement - TextMediaElementsToMedias/
            this.HasRequired(t => t.TextMediaElement)
                .WithMany(a => a.TextMediaElementsToMedias)
                .HasForeignKey(t => t.TextMediaElementId)
                .WillCascadeOnDelete(true);

            // One-to-Many /Media - TextMediaElementsToMedias/
            this.HasRequired(t => t.Media)
                .WithMany(a => a.TextMediaElementsToMedias)
                .HasForeignKey(t => t.MediaId)
                .WillCascadeOnDelete(true);
        }
    }
}