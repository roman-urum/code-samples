using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// CustomerConstraintsAdded.
    /// </summary>
    public partial class CustomerConstraintsAdded : DbMigration
    {
        /// <summary>
        /// Ups this instance.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Customers", "Subdomain", c => c.String(nullable: false, maxLength: 63));
            CreateIndex("dbo.Customers", "Name", unique: true, name: "IX_NAME");
            CreateIndex("dbo.Customers", "Subdomain", unique: true, name: "IX_SUBDOMAIN");
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.Customers", "IX_SUBDOMAIN");
            DropIndex("dbo.Customers", "IX_NAME");
            AlterColumn("dbo.Customers", "Subdomain", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
        }
    }
}