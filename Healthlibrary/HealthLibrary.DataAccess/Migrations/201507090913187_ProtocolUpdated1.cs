using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// ProtocolUpdated1.
    /// </summary>
    public partial class ProtocolUpdated1 : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Protocols", "IsDeleted", c => c.Boolean(nullable: false));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Protocols", "IsDeleted");
        }
    }
}