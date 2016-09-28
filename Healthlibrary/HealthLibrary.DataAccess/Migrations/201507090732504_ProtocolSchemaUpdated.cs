using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// ProtocolSchemaUpdated.
    /// </summary>
    public partial class ProtocolSchemaUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            DropForeignKey("dbo.Protocols", "FirstProtocolElementId", "dbo.ProtocolElements");
            DropForeignKey("dbo.Protocols", "NameDefaultStringId", "dbo.ProtocolStrings");
            DropIndex("dbo.Protocols", new[] { "NameDefaultStringId" });
            DropIndex("dbo.Protocols", new[] { "FirstProtocolElementId" });
            DropIndex("dbo.ProtocolStrings", new[] { "ProtocolId" });
            AddColumn("dbo.ProtocolElements", "IsFirstProtocolElement", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProtocolStrings", "ProtocolId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProtocolStrings", "ProtocolId");
            DropColumn("dbo.Protocols", "NameDefaultStringId");
            DropColumn("dbo.Protocols", "FirstProtocolElementId");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AddColumn("dbo.Protocols", "FirstProtocolElementId", c => c.Guid());
            AddColumn("dbo.Protocols", "NameDefaultStringId", c => c.Guid(nullable: false));
            DropIndex("dbo.ProtocolStrings", new[] { "ProtocolId" });
            AlterColumn("dbo.ProtocolStrings", "ProtocolId", c => c.Guid());
            DropColumn("dbo.ProtocolElements", "IsFirstProtocolElement");
            CreateIndex("dbo.ProtocolStrings", "ProtocolId");
            CreateIndex("dbo.Protocols", "FirstProtocolElementId");
            CreateIndex("dbo.Protocols", "NameDefaultStringId");
            AddForeignKey("dbo.Protocols", "NameDefaultStringId", "dbo.ProtocolStrings", "Id");
            AddForeignKey("dbo.Protocols", "FirstProtocolElementId", "dbo.ProtocolElements", "Id");
        }
    }
}