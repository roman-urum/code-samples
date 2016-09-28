namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LengthRestrictionForAlertSeverityName : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [AlertSeverities] SET [Name] = left([Name], 250) WHERE len([Name])>250");

            AlterColumn("dbo.AlertSeverities", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlertSeverities", "Name", c => c.String());
        }
    }
}
