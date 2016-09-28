namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstNameAndLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Principals", "FirstName", c => c.String(maxLength: 50));
            AddColumn("dbo.Principals", "LastName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Principals", "LastName");
            DropColumn("dbo.Principals", "FirstName");
        }
    }
}
