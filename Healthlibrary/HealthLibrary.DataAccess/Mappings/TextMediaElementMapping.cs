using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// TextMediaElementMapping.
    /// </summary>
    internal class TextMediaElementMapping : EntityTypeConfiguration<TextMediaElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMediaElementMapping"/> class.
        /// </summary>
        public TextMediaElementMapping()
        {
            // Table name
            this.ToTable("TextMediaElements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}