namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringStorageKeyInMedias : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medias", "StorageKey", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medias", "StorageKey", c => c.Guid(nullable: false));
        }
    }
}
