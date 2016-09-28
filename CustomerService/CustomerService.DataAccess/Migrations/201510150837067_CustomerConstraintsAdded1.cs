using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// CustomerConstraintsAdded1.
    /// </summary>
    public partial class CustomerConstraintsAdded1 : DbMigration
    {
        /// <summary>
        /// Ups this instance.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Customers", "LogoPath", c => c.String(maxLength: 1000));
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Customers", "LogoPath", c => c.String());
        }
    }
}