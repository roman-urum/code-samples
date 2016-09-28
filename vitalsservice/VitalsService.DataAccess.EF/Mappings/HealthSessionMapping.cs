using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// HealthSessionMapping.
    /// </summary>
    internal class HealthSessionMapping : EntityTypeConfiguration<HealthSession>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthSessionMapping"/> class.
        /// </summary>
        public HealthSessionMapping()
        {
            // Table name
            this.ToTable("HealthSessions");

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

            this.Property(e => e.ClientId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CLIENT_ID", 0) { IsUnique = false }));

            this.Property(e => e.ProtocolId)
                .IsOptional();

            this.Property(e => e.CalendarItemId)
                .IsOptional();

            this.Property(e => e.ScheduledTz)
                .HasMaxLength(44);

            this.Property(e => e.StartedTz)
                .HasMaxLength(44);

            this.Property(e => e.CompletedTz)
                .HasMaxLength(44);

            this.Property(e => e.ClientId)
                .HasMaxLength(50);
        }
    }
}