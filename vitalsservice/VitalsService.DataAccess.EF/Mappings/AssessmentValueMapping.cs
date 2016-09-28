using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// Db configuration for assessment values.
    /// </summary>
    public class AssessmentValueMapping : EntityTypeConfiguration<AssessmentValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentValueMapping"/> class.
        /// </summary>
        public AssessmentValueMapping()
        {
            // Table name
            this.ToTable("AssessmentValues");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.HasRequired(e => e.AssessmentMedia)
                .WithMany(m => m.AssessmentValues)
                .WillCascadeOnDelete(true);
        }
    }
}