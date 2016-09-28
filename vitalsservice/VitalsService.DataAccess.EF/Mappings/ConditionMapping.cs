using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// ConditionMapping
    /// </summary>
    internal class ConditionMapping: EntityTypeConfiguration<Condition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagsMapping"/> class.
        /// </summary>
        public ConditionMapping()
        {
            this.ToTable("Conditions");

            // Primary key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UQ_NAME_PER_CUSTOMER", 0) { IsUnique = true }))
                .IsRequired()
                .HasMaxLength(200);

            this.Property(e => e.Description)
                .IsOptional()
                .HasMaxLength(1024);

            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UQ_NAME_PER_CUSTOMER", 1) { IsUnique = true }))
                .IsRequired();
        }
    }
}