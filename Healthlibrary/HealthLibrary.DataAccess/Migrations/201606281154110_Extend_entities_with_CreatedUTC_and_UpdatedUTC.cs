namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extend_entities_with_CreatedUTC_and_UpdatedUTC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerSets", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue:DateTime.UtcNow));
            AddColumn("dbo.AnswerSets", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Elements", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Elements", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.LocalizedStrings", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.LocalizedStrings", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Medias", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Medias", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Tags", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Tags", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ProtocolElements", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ProtocolElements", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Alerts", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Alerts", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Conditions", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Conditions", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Branches", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Branches", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Protocols", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Protocols", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ProgramElements", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ProgramElements", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Programs", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Programs", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ProgramDayElements", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ProgramDayElements", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Recurrences", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.Recurrences", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.SelectionAnswerChoices", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.SelectionAnswerChoices", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.QuestionElementToScaleAnswerChoices", "CreatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.QuestionElementToScaleAnswerChoices", "UpdatedUtc", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionElementToScaleAnswerChoices", "UpdatedUtc");
            DropColumn("dbo.QuestionElementToScaleAnswerChoices", "CreatedUtc");
            DropColumn("dbo.SelectionAnswerChoices", "UpdatedUtc");
            DropColumn("dbo.SelectionAnswerChoices", "CreatedUtc");
            DropColumn("dbo.Recurrences", "UpdatedUtc");
            DropColumn("dbo.Recurrences", "CreatedUtc");
            DropColumn("dbo.ProgramDayElements", "UpdatedUtc");
            DropColumn("dbo.ProgramDayElements", "CreatedUtc");
            DropColumn("dbo.Programs", "UpdatedUtc");
            DropColumn("dbo.Programs", "CreatedUtc");
            DropColumn("dbo.ProgramElements", "UpdatedUtc");
            DropColumn("dbo.ProgramElements", "CreatedUtc");
            DropColumn("dbo.Protocols", "UpdatedUtc");
            DropColumn("dbo.Protocols", "CreatedUtc");
            DropColumn("dbo.Branches", "UpdatedUtc");
            DropColumn("dbo.Branches", "CreatedUtc");
            DropColumn("dbo.Conditions", "UpdatedUtc");
            DropColumn("dbo.Conditions", "CreatedUtc");
            DropColumn("dbo.Alerts", "UpdatedUtc");
            DropColumn("dbo.Alerts", "CreatedUtc");
            DropColumn("dbo.ProtocolElements", "UpdatedUtc");
            DropColumn("dbo.ProtocolElements", "CreatedUtc");
            DropColumn("dbo.Tags", "UpdatedUtc");
            DropColumn("dbo.Tags", "CreatedUtc");
            DropColumn("dbo.Medias", "UpdatedUtc");
            DropColumn("dbo.Medias", "CreatedUtc");
            DropColumn("dbo.LocalizedStrings", "UpdatedUtc");
            DropColumn("dbo.LocalizedStrings", "CreatedUtc");
            DropColumn("dbo.Elements", "UpdatedUtc");
            DropColumn("dbo.Elements", "CreatedUtc");
            DropColumn("dbo.AnswerSets", "UpdatedUtc");
            DropColumn("dbo.AnswerSets", "CreatedUtc");
        }
    }
}
