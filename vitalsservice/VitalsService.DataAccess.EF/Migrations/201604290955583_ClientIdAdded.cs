using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    /// <summary>
    /// ClientIdAdded.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class ClientIdAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Measurements", "ClientId", c => c.String(maxLength: 50));
            AddColumn("dbo.HealthSessions", "ClientId", c => c.String(maxLength: 50));
            CreateIndex("dbo.Measurements", "ClientId", name: "IX_CLIENT_ID");
            CreateIndex("dbo.HealthSessions", "ClientId", name: "IX_CLIENT_ID");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.HealthSessions", "IX_CLIENT_ID");
            DropIndex("dbo.Measurements", "IX_CLIENT_ID");
            DropColumn("dbo.HealthSessions", "ClientId");
            DropColumn("dbo.Measurements", "ClientId");
        }
    }
}