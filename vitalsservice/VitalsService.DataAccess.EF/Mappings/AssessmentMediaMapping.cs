using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// AlertMapping.
    /// </summary>
    internal class AssessmentMediaMapping : EntityTypeConfiguration<AssessmentMedia>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMapping" /> class.
        /// </summary>
        public AssessmentMediaMapping()
        {
            // Table name
            this.ToTable("AssessmentMedias");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.PatientId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_PATIENT_ID", 0) { IsUnique = false }));

            this.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.ContentType);

            this.Property(e => e.OriginalFileName)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.FileName);

            this.Property(e => e.StorageKey)
                .HasMaxLength(1000);

            this.Property(d => d.MediaTypeString)
                .HasColumnName("MediaType")
                .HasMaxLength(20)
                .IsRequired();

            this.Ignore(d => d.MediaType);
        }
    }
}