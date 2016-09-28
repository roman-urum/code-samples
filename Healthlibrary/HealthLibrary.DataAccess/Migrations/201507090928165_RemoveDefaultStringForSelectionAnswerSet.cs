namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDefaultStringForSelectionAnswerSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SelectionAnswerChoices", "DefaultStringId", "dbo.SelectionAnswerChoiceStrings");
            DropIndex("dbo.SelectionAnswerChoices", new[] { "DefaultStringId" });
            DropIndex("dbo.SelectionAnswerChoiceStrings", new[] { "SelectionAnswerChoiceId" });
            AlterColumn("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId");
            DropColumn("dbo.SelectionAnswerChoices", "DefaultStringId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SelectionAnswerChoices", "DefaultStringId", c => c.Guid());
            DropIndex("dbo.SelectionAnswerChoiceStrings", new[] { "SelectionAnswerChoiceId" });
            AlterColumn("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId", c => c.Guid());
            CreateIndex("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId");
            CreateIndex("dbo.SelectionAnswerChoices", "DefaultStringId");
            AddForeignKey("dbo.SelectionAnswerChoices", "DefaultStringId", "dbo.SelectionAnswerChoiceStrings", "Id");
        }
    }
}
