using System.Data.Entity.Migrations;

namespace DeviceService.DataAccess.Migrations
{
    /// <summary>
    /// CustomerIdIndexesAdded.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class CustomerIdIndexesAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateIndex("dbo.Devices", "CustomerId", name: "IX_CUSTOMER_ID");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.Devices", "IX_CUSTOMER_ID");
        }
    }
}