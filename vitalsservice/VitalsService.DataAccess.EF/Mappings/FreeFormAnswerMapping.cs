using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// Db configuration for selection answers.
    /// </summary>
    internal class FreeFormAnswerMapping : EntityTypeConfiguration<FreeFormAnswer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FreeFormAnswerMapping"/> class.
        /// </summary>
        public FreeFormAnswerMapping()
        {
            // Table name
            this.ToTable("FreeFormAnswers");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
