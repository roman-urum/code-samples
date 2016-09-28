using System.Data.Entity.Migrations;

namespace HealthLibrary.DataAccess.Migrations
{
    /// <summary>
    /// BranchUpdated.
    /// </summary>
    public partial class BranchUpdated : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Operator", c => c.Int());
            AlterColumn("dbo.Branches", "Value", c => c.Decimal(precision: 18, scale: 2));
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Branches", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Branches", "Operator", c => c.Int(nullable: false));
        }
    }
}