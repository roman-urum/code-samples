using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// AlertMapping.
    /// </summary>
    internal class AlertMapping : EntityTypeConfiguration<Alert>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMapping" /> class.
        /// </summary>
        public AlertMapping()
        {
            // Table name
            this.ToTable("Alerts");

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

            this.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(e => e.Body)
                .IsOptional()
                .HasMaxLength(5000);
        }
    }
}