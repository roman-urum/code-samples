using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// PatientConditionMapping.
    /// </summary>
    internal class PatientConditionMapping : EntityTypeConfiguration<PatientCondition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionMapping"/> class.
        /// </summary>
        public PatientConditionMapping()
        {
            // Table name
            this.ToTable("PatientConditions");

            // Primary Key
            this.HasKey(e => new { e.PatientId, e.ConditionId });

            // Properties
            this.Property(e => e.ConditionId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_PATIENT_CONDITION_ID", 0) { IsUnique = false }));

            // One-to-Many /Condition - PatientConditions/
            this.HasRequired(pc => pc.Condition)
                .WithMany(c => c.PatientConditions)
                .HasForeignKey(pc => pc.ConditionId)
                .WillCascadeOnDelete(true);
        }
    }
}