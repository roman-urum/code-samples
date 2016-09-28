using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    public class QuestionElementToSelectionAnswerChoiceMapping :  EntityTypeConfiguration<QuestionElementToSelectionAnswerChoice>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementToSelectionAnswerChoiceMapping"/> class.
        /// </summary>
        public QuestionElementToSelectionAnswerChoiceMapping()
        {
            // Table name
            this.ToTable("QuestionElementToSelectionAnswerChoices");

            // Primary Key
            this.HasKey(e => new { e.QuestionElementId, e.SelectionAnswerChoiceId });

            // Restrictions

            this.Property(e => e.InternalId)
                .HasMaxLength(100);

            this.Property(e => e.ExternalId)
                .HasMaxLength(100);

            // Relationships

            // One-to-Many /QuestionElement - SelectionAnswerChoices/
            this.HasRequired(t => t.QuestionElement)
                .WithMany(t => t.QuestionElementToSelectionAnswerChoices)
                .HasForeignKey(t => t.QuestionElementId)
                .WillCascadeOnDelete(true);

            // One-to-Many /QuestionElement - ScaleAnswerChoices/
            this.HasRequired(t => t.SelectionAnswerChoice)
                .WithMany(t => t.QuestionElementToSelectionAnswerChoices)
                .HasForeignKey(t => t.SelectionAnswerChoiceId)
                .WillCascadeOnDelete(true);
        }
    }
}
