using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VitalsService.Domain.Constants;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Mappings
{
    /// <summary>
    /// SuggestedNotableMapping.
    /// </summary>
    internal class SuggestedNotableMapping : EntityTypeConfiguration<SuggestedNotable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestedNotableMapping"/> class.
        /// </summary>
        public SuggestedNotableMapping()
        {
            // Table name
            this.ToTable("SuggestedNotables");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(DbConstraints.MaxLength.NotableName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NAME_CUSTOMERID", 0) { IsUnique = true }));

            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NAME_CUSTOMERID", 1) { IsUnique = true }));
        }
    }
}