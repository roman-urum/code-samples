using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    public class QuestionElementToScaleAnswerChoiceMapping : EntityTypeConfiguration<QuestionElementToScaleAnswerChoice>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementToScaleAnswerChoiceMapping"/> class.
        /// </summary>
        public QuestionElementToScaleAnswerChoiceMapping()
        {
            // Table name
            this.ToTable("QuestionElementToScaleAnswerChoices");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Restrictions
            this.Property(e => e.InternalId)
                .HasMaxLength(100);

            this.Property(e => e.ExternalId)
                .HasMaxLength(100);

            // Relationships

            // One-to-Many /QuestionElement - SelectionAnswerChoices/
            this.HasRequired(t => t.QuestionElement)
                .WithMany(t => t.QuestionElementToScaleAnswerChoices)
                .HasForeignKey(t => t.QuestionElementId)
                .WillCascadeOnDelete(true);
        }
    }
}