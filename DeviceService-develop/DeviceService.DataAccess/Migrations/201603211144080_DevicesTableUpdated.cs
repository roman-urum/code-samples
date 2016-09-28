using System.Data.Entity.Migrations;

namespace DeviceService.DataAccess.Migrations
{
    /// <summary>
    /// DevicesTableUpdated.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class DevicesTableUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Devices", "DeviceTz", c => c.String(maxLength: 44));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Devices", "DeviceTz");
        }
    }
}