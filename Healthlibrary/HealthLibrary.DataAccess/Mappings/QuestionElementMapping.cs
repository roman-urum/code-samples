using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// QuestionElementMapping.
    /// </summary>
    internal class QuestionElementMapping : EntityTypeConfiguration<QuestionElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementMapping"/> class.
        /// </summary>
        public QuestionElementMapping()
        {
            // Table name
            this.ToTable("QuestionElements");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Properties
            this.Property(e => e.InternalId)
                .HasMaxLength(100);

            this.Property(e => e.ExternalId)
                .HasMaxLength(100);

            // Relationships
            // One-to-Many /AnswerSet - QuestionElements/
            this.HasRequired(t => t.AnswerSet)
                .WithMany(t => t.QuestionElements)
                .WillCascadeOnDelete(false);
        }
    }
}