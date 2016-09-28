using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// ElementMapping.
    /// </summary>
    internal class ElementMapping : EntityTypeConfiguration<Element>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementMapping"/> class.
        /// </summary>
        public ElementMapping()
        {
            // Table name
            this.ToTable("Elements");

            // Properties
            this.Property(e => e.CustomerId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_CUSTOMER_ID", 0) { IsUnique = false }));
        }
    }
}