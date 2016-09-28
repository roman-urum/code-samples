using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    /// <summary>
    /// SuggestedNotablesIndexUpdated.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class SuggestedNotablesIndexUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            DropIndex("dbo.SuggestedNotables", "IX_NAME");
            CreateIndex("dbo.SuggestedNotables", new[] { "Name", "CustomerId" }, unique: true, name: "IX_NAME_CUSTOMERID");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.SuggestedNotables", "IX_NAME_CUSTOMERID");
            CreateIndex("dbo.SuggestedNotables", "Name", unique: true, name: "IX_NAME");
        }
    }
}