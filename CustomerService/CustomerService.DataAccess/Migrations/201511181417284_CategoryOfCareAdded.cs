namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryOfCareAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriesOfCare",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.Name, unique: true, name: "IX_NAME")
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.SitesToCategoriesOfCare",
                c => new
                    {
                        SiteId = c.Guid(nullable: false),
                        CategoryOfCareId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SiteId, t.CategoryOfCareId })
                .ForeignKey("dbo.CategoriesOfCare", t => t.SiteId, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.CategoryOfCareId, cascadeDelete: true)
                .Index(t => t.SiteId)
                .Index(t => t.CategoryOfCareId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SitesToCategoriesOfCare", "CategoryOfCareId", "dbo.Sites");
            DropForeignKey("dbo.SitesToCategoriesOfCare", "SiteId", "dbo.CategoriesOfCare");
            DropForeignKey("dbo.CategoriesOfCare", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SitesToCategoriesOfCare", new[] { "CategoryOfCareId" });
            DropIndex("dbo.SitesToCategoriesOfCare", new[] { "SiteId" });
            DropIndex("dbo.CategoriesOfCare", new[] { "CustomerId" });
            DropIndex("dbo.CategoriesOfCare", "IX_NAME");
            DropTable("dbo.SitesToCategoriesOfCare");
            DropTable("dbo.CategoriesOfCare");
        }
    }
}
