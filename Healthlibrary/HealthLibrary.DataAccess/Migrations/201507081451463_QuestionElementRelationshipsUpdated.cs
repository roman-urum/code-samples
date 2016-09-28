namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionElementRelationshipsUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionElements", "Id", "dbo.AnswerSets");
            AddColumn("dbo.QuestionElements", "AnswerSetId", c => c.Guid(nullable: false));
            CreateIndex("dbo.QuestionElements", "AnswerSetId");
            AddForeignKey("dbo.QuestionElements", "AnswerSetId", "dbo.AnswerSets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionElements", "AnswerSetId", "dbo.AnswerSets");
            DropIndex("dbo.QuestionElements", new[] { "AnswerSetId" });
            DropColumn("dbo.QuestionElements", "AnswerSetId");
            AddForeignKey("dbo.QuestionElements", "Id", "dbo.AnswerSets", "Id", cascadeDelete: true);
        }
    }
}
