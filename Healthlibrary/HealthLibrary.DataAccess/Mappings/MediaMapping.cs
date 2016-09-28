using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// MediaMapping.
    /// </summary>
    internal class MediaMapping : EntityTypeConfiguration<Media>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaMapping"/> class.
        /// </summary>
        public MediaMapping()
        {
            // Table name
            this.ToTable("Medias");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(e => e.Description)
                .HasMaxLength(1000);

            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(e => e.OriginalFileName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(e => e.OriginalStorageKey)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(e => e.ThumbnailStorageKey)
                .HasMaxLength(1000);

            // Many-to-Many /Medias - Tags/
            this.HasMany(t => t.Tags)
                .WithMany(a => a.LocalizedMedias)
                .Map(m =>
                {
                    m.ToTable("MediasToTags");
                    m.MapLeftKey("MediaId");
                    m.MapRightKey("TagId");
                });
        }
    }
}