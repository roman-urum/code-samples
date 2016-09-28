namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdsAndScoresForQuestionElement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionElementToSelectionAnswerChoices",
                c => new
                    {
                        QuestionElementId = c.Guid(nullable: false),
                        SelectionAnswerChoiceId = c.Guid(nullable: false),
                        ExternalId = c.String(maxLength: 100),
                        InternalId = c.String(maxLength: 100),
                        ExternalScore = c.Int(nullable: false),
                        InternalScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionElementId, t.SelectionAnswerChoiceId })
                .ForeignKey("dbo.QuestionElements", t => t.QuestionElementId)
                .ForeignKey("dbo.SelectionAnswerChoices", t => t.SelectionAnswerChoiceId, cascadeDelete: true)
                .Index(t => t.QuestionElementId)
                .Index(t => t.SelectionAnswerChoiceId);
            
            CreateTable(
                "dbo.QuestionElementToScaleAnswerChoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionElementId = c.Guid(nullable: false),
                        ExternalId = c.String(maxLength: 100),
                        Value = c.Int(nullable: false),
                        InternalId = c.String(maxLength: 100),
                        ExternalScore = c.Int(),
                        InternalScore = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionElements", t => t.QuestionElementId)
                .Index(t => t.QuestionElementId);
            
            AddColumn("dbo.QuestionElements", "InternalId", c => c.String(maxLength: 100));
            AddColumn("dbo.QuestionElements", "ExternalId", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionElementToScaleAnswerChoices", "QuestionElementId", "dbo.QuestionElements");
            DropForeignKey("dbo.QuestionElementToSelectionAnswerChoices", "SelectionAnswerChoiceId", "dbo.SelectionAnswerChoices");
            DropForeignKey("dbo.QuestionElementToSelectionAnswerChoices", "QuestionElementId", "dbo.QuestionElements");
            DropIndex("dbo.QuestionElementToScaleAnswerChoices", new[] { "QuestionElementId" });
            DropIndex("dbo.QuestionElementToSelectionAnswerChoices", new[] { "SelectionAnswerChoiceId" });
            DropIndex("dbo.QuestionElementToSelectionAnswerChoices", new[] { "QuestionElementId" });
            DropColumn("dbo.QuestionElements", "ExternalId");
            DropColumn("dbo.QuestionElements", "InternalId");
            DropTable("dbo.QuestionElementToScaleAnswerChoices");
            DropTable("dbo.QuestionElementToSelectionAnswerChoices");
        }
    }
}
