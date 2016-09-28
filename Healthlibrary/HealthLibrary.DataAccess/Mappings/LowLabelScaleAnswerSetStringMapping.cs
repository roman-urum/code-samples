using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// LowLabelScaleAnswerSetStringMapping.
    /// </summary>
    internal class LowLabelScaleAnswerSetStringMapping : EntityTypeConfiguration<LowLabelScaleAnswerSetString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LowLabelScaleAnswerSetStringMapping"/> class.
        /// </summary>
        public LowLabelScaleAnswerSetStringMapping()
        {
            // Table name
            this.ToTable("LowLabelScaleAnswerSetStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /ScaleAnswerSet - LowLabelScaleAnswerSetStrings/
            this.HasOptional(t => t.ScaleAnswerSet)
                .WithMany(a => a.LowLabelScaleAnswerSetStrings)
                .HasForeignKey(t => t.ScaleAnswerSetId)
                .WillCascadeOnDelete(true);
        }
    }
}