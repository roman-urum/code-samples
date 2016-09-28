using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// CustomerSoftDelete.
    /// </summary>
    public partial class CustomerSoftDelete : DbMigration
    {
        /// <summary>
        /// Ups this instance.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsDeleted", c => c.Boolean(nullable: false));
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsDeleted");
        }
    }
}