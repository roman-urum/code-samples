using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// SiteSchemaUpdate.
    /// </summary>
    public partial class SiteSchemaUpdate : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Sites", "State", c => c.String(maxLength: 100));
            AddColumn("dbo.Sites", "City", c => c.String(maxLength: 50));
            AddColumn("dbo.Sites", "Address1", c => c.String(maxLength: 250));
            AddColumn("dbo.Sites", "Address2", c => c.String(maxLength: 250));
            AddColumn("dbo.Sites", "Address3", c => c.String(maxLength: 250));
            AddColumn("dbo.Sites", "ZipCode", c => c.String(maxLength: 10));
            AddColumn("dbo.Sites", "NationalProviderIdentificator", c => c.String(maxLength: 100));
            AddColumn("dbo.Sites", "CustomerSiteId", c => c.String(maxLength: 100));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Sites", "CustomerSiteId");
            DropColumn("dbo.Sites", "NationalProviderIdentificator");
            DropColumn("dbo.Sites", "ZipCode");
            DropColumn("dbo.Sites", "Address3");
            DropColumn("dbo.Sites", "Address2");
            DropColumn("dbo.Sites", "Address1");
            DropColumn("dbo.Sites", "City");
            DropColumn("dbo.Sites", "State");
        }
    }
}