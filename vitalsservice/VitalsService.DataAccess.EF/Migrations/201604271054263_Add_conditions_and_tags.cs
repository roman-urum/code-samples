namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_conditions_and_tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 1024),
                        IsDeleted = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CustomerId, name: "IX_CUSTOMER_ID");
            
            CreateTable(
                "dbo.ConditionsToTags",
                c => new
                    {
                        ConditionId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConditionId, t.TagId })
                .ForeignKey("dbo.Tags", t => t.ConditionId, cascadeDelete: true)
                .ForeignKey("dbo.Conditions", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ConditionId)
                .Index(t => t.TagId);
            
            AddColumn("dbo.DefaultThresholds", "ConditionId", c => c.Guid());
            CreateIndex("dbo.DefaultThresholds", "ConditionId");
            AddForeignKey("dbo.DefaultThresholds", "ConditionId", "dbo.Conditions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaultThresholds", "ConditionId", "dbo.Conditions");
            DropForeignKey("dbo.ConditionsToTags", "TagId", "dbo.Conditions");
            DropForeignKey("dbo.ConditionsToTags", "ConditionId", "dbo.Tags");
            DropIndex("dbo.DefaultThresholds", new[] { "ConditionId" });
            DropIndex("dbo.ConditionsToTags", new[] { "TagId" });
            DropIndex("dbo.ConditionsToTags", new[] { "ConditionId" });
            DropIndex("dbo.Tags", "IX_CUSTOMER_ID");
            DropColumn("dbo.DefaultThresholds", "ConditionId");
            DropTable("dbo.ConditionsToTags");
            DropTable("dbo.Tags");
            DropTable("dbo.Conditions");
        }
    }
}
