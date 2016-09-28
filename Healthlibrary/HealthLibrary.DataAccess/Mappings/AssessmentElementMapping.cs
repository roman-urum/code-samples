using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// MeasurementElementMapping.
    /// </summary>
    internal class AssessmentElementMapping : EntityTypeConfiguration<AssessmentElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentElementMapping"/> class.
        /// </summary>
        public AssessmentElementMapping()
        {
            // Table name
            this.ToTable("AssessmentElements");

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(d => d.AssessmentTypeString)
                .HasColumnName("AssessmentType")
                .HasMaxLength(20)
                .IsRequired();

            this.Ignore(d => d.AssessmentType);

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}