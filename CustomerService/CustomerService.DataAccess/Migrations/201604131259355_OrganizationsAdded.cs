using System.Data.Entity.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    /// <summary>
    /// OrganizationsAdded.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class OrganizationsAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        CustomerId = c.Int(nullable: false),
                        ParentOrganizationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Organizations", t => t.ParentOrganizationId)
                .Index(t => t.Name, unique: true, name: "IX_NAME")
                .Index(t => t.CustomerId)
                .Index(t => t.ParentOrganizationId);
            
            AddColumn("dbo.Sites", "OrganizationId", c => c.Guid());
            CreateIndex("dbo.Sites", "OrganizationId");
            AddForeignKey("dbo.Sites", "OrganizationId", "dbo.Organizations", "Id");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "ParentOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Organizations", new[] { "ParentOrganizationId" });
            DropIndex("dbo.Organizations", new[] { "CustomerId" });
            DropIndex("dbo.Organizations", "IX_NAME");
            DropIndex("dbo.Sites", new[] { "OrganizationId" });
            DropColumn("dbo.Sites", "OrganizationId");
            DropTable("dbo.Organizations");
        }
    }
}