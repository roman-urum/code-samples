using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// ProtocolUpdatedWithLocalizedStrings.
    /// </summary>
    public partial class ProtocolUpdatedWithLocalizedStrings : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.ProtocolStrings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProtocolId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocalizedStrings", t => t.Id)
                .ForeignKey("dbo.Protocols", t => t.ProtocolId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ProtocolId);
            
            AddColumn("dbo.Protocols", "NameDefaultStringId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Protocols", "NameDefaultStringId");
            AddForeignKey("dbo.Protocols", "NameDefaultStringId", "dbo.ProtocolStrings", "Id");
            DropColumn("dbo.Protocols", "Name");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AddColumn("dbo.Protocols", "Name", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.ProtocolStrings", "ProtocolId", "dbo.Protocols");
            DropForeignKey("dbo.ProtocolStrings", "Id", "dbo.LocalizedStrings");
            DropForeignKey("dbo.Protocols", "NameDefaultStringId", "dbo.ProtocolStrings");
            DropIndex("dbo.ProtocolStrings", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolStrings", new[] { "Id" });
            DropIndex("dbo.Protocols", new[] { "NameDefaultStringId" });
            DropColumn("dbo.Protocols", "NameDefaultStringId");
            DropTable("dbo.ProtocolStrings");
        }
    }
}