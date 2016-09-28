using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    /// <summary>
    /// ConditionsUpdated.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class ConditionsUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            DropColumn("dbo.Conditions", "IsDeleted");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AddColumn("dbo.Conditions", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}