namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSiteIdTypeToNonIdentity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Sites");
            AlterColumn("dbo.Sites", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Sites");
            AlterColumn("dbo.Sites", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Sites", "Id");
        }
    }
}
