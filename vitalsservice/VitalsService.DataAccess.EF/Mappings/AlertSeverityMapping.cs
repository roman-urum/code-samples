using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// AlertSeverityMapping.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{VitalsService.Domain.DbEntities.AlertSeverity}" />
    public class AlertSeverityMapping : EntityTypeConfiguration<AlertSeverity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertSeverityMapping"/> class.
        /// </summary>
        public AlertSeverityMapping()
        {
            // Table name
            this.ToTable("AlertSeverities");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));

            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.AlertSeverityName);
        }
    }
}