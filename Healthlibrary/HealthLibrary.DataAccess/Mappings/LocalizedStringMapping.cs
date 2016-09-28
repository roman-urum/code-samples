using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// LocalizedStringMapping.
    /// </summary>
    internal class LocalizedStringMapping : EntityTypeConfiguration<LocalizedString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedStringMapping"/> class.
        /// </summary>
        public LocalizedStringMapping()
        {
            // Table name
            this.ToTable("LocalizedStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Value)
                .HasMaxLength(1000);

            this.Property(e => e.Description)
                .HasMaxLength(DbConstraints.MaxLength.Description);

            this.Property(e => e.Pronunciation)
                .HasMaxLength(1000);

            this.Property(e => e.Language)
                .IsRequired()
                .HasMaxLength(5);

            // One-to-Many /LocalizedMedia - LocalizedStrings/
            this.HasOptional(t => t.AudioFileMedia)
                .WithMany(a => a.LocalizedStrings)
                .HasForeignKey(t => t.AudioFileMediaId)
                .WillCascadeOnDelete(false);
        }
    }
}