namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extenddefaultthresholdwithconditionid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DefaultThresholds", "ConditionId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DefaultThresholds", "ConditionId");
        }
    }
}
