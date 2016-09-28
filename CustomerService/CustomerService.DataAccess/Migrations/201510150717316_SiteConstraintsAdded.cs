using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// SiteConstraintsAdded.
    /// </summary>
    public partial class SiteConstraintsAdded : DbMigration
    {
        /// <summary>
        /// Ups this instance.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Sites", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Sites", "ContactPhone", c => c.String(maxLength: 20));
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Sites", "ContactPhone", c => c.String());
            AlterColumn("dbo.Sites", "Name", c => c.String());
        }
    }
}