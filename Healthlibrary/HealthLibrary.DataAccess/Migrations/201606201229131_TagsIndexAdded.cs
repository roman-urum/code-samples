using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    public partial class TagsIndexAdded : DbMigration
    {
        public override void Up()
        {
            // Deleting duplicated Tags
            Sql("DELETE FROM Tags WHERE Id NOT IN (SELECT MAX(Id) FROM Tags GROUP BY Name, CustomerId HAVING MAX(Id) IS NOT NULL)");

            DropIndex("dbo.Tags", "IX_CUSTOMER_ID");
            RenameColumn(table: "dbo.ElementsToTags", name: "ElementId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ElementsToTags", name: "TagId", newName: "ElementId");
            RenameColumn(table: "dbo.ElementsToTags", name: "__mig_tmp__0", newName: "TagId");
            RenameIndex(table: "dbo.ElementsToTags", name: "IX_ElementId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ElementsToTags", name: "IX_TagId", newName: "IX_ElementId");
            RenameIndex(table: "dbo.ElementsToTags", name: "__mig_tmp__0", newName: "IX_TagId");
            CreateIndex("dbo.Tags", new[] { "CustomerId", "Name" }, unique: true, name: "IX_CUSTOMER_ID_NAME");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", "IX_CUSTOMER_ID_NAME");
            RenameIndex(table: "dbo.ElementsToTags", name: "IX_TagId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ElementsToTags", name: "IX_ElementId", newName: "IX_TagId");
            RenameIndex(table: "dbo.ElementsToTags", name: "__mig_tmp__0", newName: "IX_ElementId");
            RenameColumn(table: "dbo.ElementsToTags", name: "TagId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ElementsToTags", name: "ElementId", newName: "TagId");
            RenameColumn(table: "dbo.ElementsToTags", name: "__mig_tmp__0", newName: "ElementId");
            CreateIndex("dbo.Tags", "CustomerId", name: "IX_CUSTOMER_ID");
        }
    }
}