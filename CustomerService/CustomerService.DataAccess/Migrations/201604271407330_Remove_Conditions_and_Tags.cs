namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Conditions_and_Tags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConditionsToTags", "ConditionId", "dbo.Tags");
            DropForeignKey("dbo.ConditionsToTags", "TagId", "dbo.Conditions");
            DropForeignKey("dbo.Tags", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Conditions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Conditions", new[] { "CustomerId" });
            DropIndex("dbo.Tags", "IX_CUSTOMER_ID");
            DropIndex("dbo.ConditionsToTags", new[] { "ConditionId" });
            DropIndex("dbo.ConditionsToTags", new[] { "TagId" });
            DropTable("dbo.Conditions");
            DropTable("dbo.Tags");
            DropTable("dbo.ConditionsToTags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ConditionsToTags",
                c => new
                    {
                        ConditionId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConditionId, t.TagId });
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateIndex("dbo.ConditionsToTags", "TagId");
            CreateIndex("dbo.ConditionsToTags", "ConditionId");
            CreateIndex("dbo.Tags", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Conditions", "CustomerId");
            AddForeignKey("dbo.Conditions", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Tags", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.ConditionsToTags", "TagId", "dbo.Conditions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConditionsToTags", "ConditionId", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
