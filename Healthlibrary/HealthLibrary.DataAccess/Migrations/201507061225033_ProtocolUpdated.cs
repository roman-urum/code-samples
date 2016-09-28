using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// ProtocolUpdated.
    /// </summary>
    public partial class ProtocolUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Protocols", "Name", c => c.String(nullable: false, maxLength: 100));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Protocols", "Name");
        }
    }
}