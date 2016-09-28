namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Principals", "Description", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Principals", "Description", c => c.String());
        }
    }
}
