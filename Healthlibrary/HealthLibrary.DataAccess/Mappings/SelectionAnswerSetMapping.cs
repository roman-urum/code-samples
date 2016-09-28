using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// SelectionAnswerSetMapping.
    /// </summary>
    internal class SelectionAnswerSetMapping : EntityTypeConfiguration<SelectionAnswerSet>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionAnswerSetMapping"/> class.
        /// </summary>
        public SelectionAnswerSetMapping()
        {
            // Table name
            this.ToTable("SelectionAnswerSets");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}