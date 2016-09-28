using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// Db configuration for scale answers.
    /// </summary>
    internal class ScaleAnswerMapping : EntityTypeConfiguration<ScaleAnswer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleAnswerMapping"/> class.
        /// </summary>
        public ScaleAnswerMapping()
        {
            // Table name
            this.ToTable("ScaleAnswers");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}