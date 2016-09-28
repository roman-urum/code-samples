using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// PatientThresholdMapping.
    /// </summary>
    internal class PatientThresholdMapping : EntityTypeConfiguration<PatientThreshold>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientThresholdMapping" /> class.
        /// </summary>
        public PatientThresholdMapping()
        {
            // Table name
            this.ToTable("PatientThresholds");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.PatientId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_PATIENT_ID", 0) { IsUnique = false }));
        }
    }
}