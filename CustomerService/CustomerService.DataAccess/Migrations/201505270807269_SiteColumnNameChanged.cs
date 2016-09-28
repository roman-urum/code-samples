using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// SiteColumnNameChanged.
    /// </summary>
    public partial class SiteColumnNameChanged : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            RenameColumn(table: "dbo.Sites", name: "NationalProviderIdentificator", newName: "NPI");
            RenameIndex(table: "dbo.Sites", name: "IX_NationalProviderIdentificator", newName: "IX_NPI");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            RenameIndex(table: "dbo.Sites", name: "IX_NPI", newName: "IX_NationalProviderIdentificator");
            RenameColumn(table: "dbo.Sites", name: "NPI", newName: "NationalProviderIdentificator");
        }
    }
}