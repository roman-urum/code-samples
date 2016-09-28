namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerIdForAnswerSet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerSets", "CustomerId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnswerSets", "CustomerId");
        }
    }
}
