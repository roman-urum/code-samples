using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ScaleAnswerSetMapping.
    /// </summary>
    internal class ScaleAnswerSetMapping : EntityTypeConfiguration<ScaleAnswerSet>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleAnswerSetMapping"/> class.
        /// </summary>
        public ScaleAnswerSetMapping()
        {
            // Table name
            this.ToTable("ScaleAnswerSets");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}