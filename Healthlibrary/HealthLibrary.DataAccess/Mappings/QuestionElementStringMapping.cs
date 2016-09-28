using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// QuestionElementStringMapping.
    /// </summary>
    internal class QuestionElementStringMapping : EntityTypeConfiguration<QuestionElementString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionElementStringMapping"/> class.
        /// </summary>
        public QuestionElementStringMapping()
        {
            // Table name
            this.ToTable("QuestionElementStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /QuestionElement - QuestionElementStrings/
            this.HasRequired(t => t.QuestionElement)
                .WithMany(a => a.LocalizedStrings)
                .HasForeignKey(t => t.QuestionElementId)
                .WillCascadeOnDelete(true);
        }
    }
}