namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeScoresNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionElementToSelectionAnswerChoices", "ExternalScore", c => c.Int());
            AlterColumn("dbo.QuestionElementToSelectionAnswerChoices", "InternalScore", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuestionElementToSelectionAnswerChoices", "InternalScore", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionElementToSelectionAnswerChoices", "ExternalScore", c => c.Int(nullable: false));
        }
    }
}
