using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// SiteEntityIndexesAdded.
    /// </summary>
    public partial class SiteEntityIndexesAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateIndex("dbo.Sites", "NationalProviderIdentificator");
            CreateIndex("dbo.Sites", "CustomerSiteId");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.Sites", new[] { "CustomerSiteId" });
            DropIndex("dbo.Sites", new[] { "NationalProviderIdentificator" });
        }
    }
}