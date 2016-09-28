using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// OrganizationsUpdated.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class OrganizationsUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            DropForeignKey("dbo.Organizations", "CustomerId", "dbo.Customers");
            RenameColumn(table: "dbo.Sites", name: "OrganizationId", newName: "ParentOrganizationId");
            RenameIndex(table: "dbo.Sites", name: "IX_OrganizationId", newName: "IX_ParentOrganizationId");
            AddForeignKey("dbo.Organizations", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Organizations", "CustomerId", "dbo.Customers");
            RenameIndex(table: "dbo.Sites", name: "IX_ParentOrganizationId", newName: "IX_OrganizationId");
            RenameColumn(table: "dbo.Sites", name: "ParentOrganizationId", newName: "OrganizationId");
            AddForeignKey("dbo.Organizations", "CustomerId", "dbo.Customers", "Id");
        }
    }
}