using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// ProtocolUpdated2.
    /// </summary>
    public partial class ProtocolUpdated2 : DbMigration
    {
        /// <summary>
        /// Ups this instance.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Protocols", "IsPrivate", c => c.Boolean(nullable: false));
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Protocols", "IsPrivate");
        }
    }
}