using System.Data.Entity.Migrations;

namespace Maestro.DataAccess.EF.Migrations
{
    /// <summary>
    /// CustomerUserUpdated.
    /// </summary>
    public partial class CustomerUserUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Users", "IsDeleted");
        }
    }
}