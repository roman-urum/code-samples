using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// OrganizationsAndSitesAreSoftDelitable.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class OrganizationsAndSitesAreSoftDelitable : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Sites", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Organizations", "IsDeleted", c => c.Boolean(nullable: false));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Organizations", "IsDeleted");
            DropColumn("dbo.Sites", "IsDeleted");
        }
    }
}