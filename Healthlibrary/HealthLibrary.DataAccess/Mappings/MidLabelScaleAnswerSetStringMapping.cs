using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// MidLabelScaleAnswerSetStringMapping.
    /// </summary>
    internal class MidLabelScaleAnswerSetStringMapping : EntityTypeConfiguration<MidLabelScaleAnswerSetString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MidLabelScaleAnswerSetStringMapping"/> class.
        /// </summary>
        public MidLabelScaleAnswerSetStringMapping()
        {
            // Table name
            this.ToTable("MidLabelScaleAnswerSetStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /ScaleAnswerSet - MidLabelScaleAnswerSetStrings/
            this.HasOptional(t => t.ScaleAnswerSet)
                .WithMany(a => a.MidLabelScaleAnswerSetStrings)
                .HasForeignKey(t => t.ScaleAnswerSetId)
                .WillCascadeOnDelete(true);
        }
    }
}