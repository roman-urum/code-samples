namespace CustomerService.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSiteIdTypeToGuid : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM dbo.Sites");
            DropPrimaryKey("dbo.Sites");
            DropColumn("dbo.Sites", "Id");
            AddColumn("dbo.Sites", "Id", cb => cb.Guid(nullable:false));
            //AlterColumn("dbo.Sites", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Sites", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Sites");
            //AlterColumn("dbo.Sites", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Sites", "Id");
            AddColumn("dbo.Sites", "Id", cb => cb.Int(nullable: false, identity:true));
            AddPrimaryKey("dbo.Sites", "Id");
        }
    }
}
