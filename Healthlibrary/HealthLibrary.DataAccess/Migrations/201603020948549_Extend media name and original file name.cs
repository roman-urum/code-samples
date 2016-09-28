namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extendmedianameandoriginalfilename : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
