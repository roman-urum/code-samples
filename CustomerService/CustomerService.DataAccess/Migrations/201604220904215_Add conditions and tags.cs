namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addconditionsandtags : DbMigration
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conditions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Tags", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ConditionsToTags", "TagId", "dbo.Conditions");
            DropForeignKey("dbo.ConditionsToTags", "ConditionId", "dbo.Tags");
            DropIndex("dbo.ConditionsToTags", new[] { "TagId" });
            DropIndex("dbo.ConditionsToTags", new[] { "ConditionId" });
            DropIndex("dbo.Tags", "IX_CUSTOMER_ID");
            DropIndex("dbo.Conditions", new[] { "CustomerId" });
            DropTable("dbo.ConditionsToTags");
            DropTable("dbo.Tags");
            DropTable("dbo.Conditions");
        }
    }
}
