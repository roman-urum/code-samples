namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaTableUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Medias", "OriginalStorageKey", c => c.String(nullable: false, maxLength: 1000));
            AddColumn("dbo.Medias", "ThumbnailStorageKey", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Medias", "StorageKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medias", "StorageKey", c => c.String());
            AlterColumn("dbo.Medias", "Name", c => c.String());
            DropColumn("dbo.Medias", "ThumbnailStorageKey");
            DropColumn("dbo.Medias", "OriginalStorageKey");
            DropColumn("dbo.Medias", "OriginalFileName");
        }
    }
}