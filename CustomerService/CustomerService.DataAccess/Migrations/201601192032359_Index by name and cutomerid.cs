namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Indexbynameandcutomerid : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CategoriesOfCare", "IX_NAME");
            CreateIndex("dbo.CategoriesOfCare", new[] { "Name", "CustomerId" }, true, "IX_NAME_CUSTOMERID");
        }

        public override void Down()
        {
            DropIndex("dbo.CategoriesOfCare", "IX_NAME_CUSTOMERID");
            CreateIndex("dbo.CategoriesOfCare", "Name", true, "IX_NAME");
        }
    }
}
