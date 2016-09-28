namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDefaultStringForQuestionElement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionElements", "DefaultStringId", "dbo.QuestionElementStrings");
            DropIndex("dbo.QuestionElementStrings", new[] { "QuestionElementId" });
            DropIndex("dbo.QuestionElements", new[] { "DefaultStringId" });
            AlterColumn("dbo.QuestionElementStrings", "QuestionElementId", c => c.Guid(nullable: false));
            CreateIndex("dbo.QuestionElementStrings", "QuestionElementId");
            DropColumn("dbo.QuestionElements", "DefaultStringId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionElements", "DefaultStringId", c => c.Guid(nullable: false));
            DropIndex("dbo.QuestionElementStrings", new[] { "QuestionElementId" });
            AlterColumn("dbo.QuestionElementStrings", "QuestionElementId", c => c.Guid());
            CreateIndex("dbo.QuestionElements", "DefaultStringId");
            CreateIndex("dbo.QuestionElementStrings", "QuestionElementId");
            AddForeignKey("dbo.QuestionElements", "DefaultStringId", "dbo.QuestionElementStrings", "Id");
        }
    }
}
