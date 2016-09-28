namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extendhealtyhsessionelementwithmediaproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthSessionElements", "MediaType", c => c.Int());
            AddColumn("dbo.HealthSessionElements", "MediaId", c => c.Guid());
            AddColumn("dbo.HealthSessionElements", "MediaName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthSessionElements", "MediaName");
            DropColumn("dbo.HealthSessionElements", "MediaId");
            DropColumn("dbo.HealthSessionElements", "MediaType");
        }
    }
}
