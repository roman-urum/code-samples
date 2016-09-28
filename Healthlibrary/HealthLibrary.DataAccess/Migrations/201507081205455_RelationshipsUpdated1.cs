namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipsUpdated1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Protocols", new[] { "FirstProtocolElementId" });
            AlterColumn("dbo.Protocols", "FirstProtocolElementId", c => c.Guid());
            CreateIndex("dbo.Protocols", "FirstProtocolElementId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Protocols", new[] { "FirstProtocolElementId" });
            AlterColumn("dbo.Protocols", "FirstProtocolElementId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Protocols", "FirstProtocolElementId");
        }
    }
}
