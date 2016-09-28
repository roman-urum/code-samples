using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// TextMediaElementStringMapping.
    /// </summary>
    internal class TextMediaElementStringMapping : EntityTypeConfiguration<TextMediaElementString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMediaElementStringMapping"/> class.
        /// </summary>
        public TextMediaElementStringMapping()
        {
            // Table name
            this.ToTable("TextMediaElementStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /TextMediaElement - TextMediaElementStrings/
            this.HasOptional(t => t.TextMediaElement)
                .WithMany(a => a.TextLocalizedStrings)
                .HasForeignKey(t => t.TextMediaElementId)
                .WillCascadeOnDelete(true);
        }
    }
}