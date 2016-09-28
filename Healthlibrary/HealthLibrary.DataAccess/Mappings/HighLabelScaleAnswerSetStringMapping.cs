using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// HighLabelScaleAnswerSetStringMapping.
    /// </summary>
    internal class HighLabelScaleAnswerSetStringMapping : EntityTypeConfiguration<HighLabelScaleAnswerSetString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighLabelScaleAnswerSetStringMapping"/> class.
        /// </summary>
        public HighLabelScaleAnswerSetStringMapping()
        {
            // Table name
            this.ToTable("HighLabelScaleAnswerSetStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /ScaleAnswerSet - HighLabelScaleAnswerSetStrings/
            this.HasOptional(t => t.ScaleAnswerSet)
                .WithMany(a => a.HighLabelScaleAnswerSetStrings)
                .HasForeignKey(t => t.ScaleAnswerSetId)
                .WillCascadeOnDelete(true);
        }
    }
}