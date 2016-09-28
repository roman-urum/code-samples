namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableFieldsForSelectionAnswerSet : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LocalizedStrings", new[] { "AudioFileMediaId" });
            DropIndex("dbo.SelectionAnswerChoiceStrings", new[] { "SelectionAnswerChoiceId" });
            AlterColumn("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId", c => c.Guid());
            AlterColumn("dbo.LocalizedStrings", "AudioFileMediaId", c => c.Guid());
            CreateIndex("dbo.LocalizedStrings", "AudioFileMediaId");
            CreateIndex("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SelectionAnswerChoiceStrings", new[] { "SelectionAnswerChoiceId" });
            DropIndex("dbo.LocalizedStrings", new[] { "AudioFileMediaId" });
            AlterColumn("dbo.LocalizedStrings", "AudioFileMediaId", c => c.Guid(nullable: false));
            AlterColumn("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SelectionAnswerChoiceStrings", "SelectionAnswerChoiceId");
            CreateIndex("dbo.LocalizedStrings", "AudioFileMediaId");
        }
    }
}
