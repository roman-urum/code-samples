namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnswerSets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerSets", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.SelectionAnswerChoices", "InternalIdentifier");
            DropColumn("dbo.SelectionAnswerChoices", "ExternalIdentifier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SelectionAnswerChoices", "ExternalIdentifier", c => c.String());
            AddColumn("dbo.SelectionAnswerChoices", "InternalIdentifier", c => c.String());
            DropColumn("dbo.AnswerSets", "IsDeleted");
        }
    }
}
