using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DataAccess.Mappings
{
    /// <summary>
    /// SelectionAnswerChoiceStringMapping.
    /// </summary>
    internal class SelectionAnswerChoiceStringMapping : EntityTypeConfiguration<SelectionAnswerChoiceString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionAnswerChoiceStringMapping"/> class.
        /// </summary>
        public SelectionAnswerChoiceStringMapping()
        {
            // Table name
            this.ToTable("SelectionAnswerChoiceStrings");

            // Primary Key
            this.HasKey(e => e.Id);
            this.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // One-to-Many /SelectionAnswerChoice - SelectionAnswerChoiceStrings/
            this.HasRequired(t => t.SelectionAnswerChoice)
                .WithMany(a => a.LocalizedStrings)
                .HasForeignKey(t => t.SelectionAnswerChoiceId)
                .WillCascadeOnDelete(true);
        }
    }
}