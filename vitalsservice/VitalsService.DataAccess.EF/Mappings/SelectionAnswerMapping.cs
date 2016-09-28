using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// Db configuration for selection answers.
    /// </summary>
    internal class SelectionAnswerMapping : EntityTypeConfiguration<SelectionAnswer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionAnswerMapping"/> class.
        /// </summary>
        public SelectionAnswerMapping()
        {
            // Table name
            this.ToTable("SelectionAnswers");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}