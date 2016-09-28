namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Setmedianameandoriginalfilenameto200characters : DbMigration
    {
        public override void Up()
        {
            Sql("update medias set OriginalFileName = substring(OriginalFileName, 0, 200) where len(OriginalFileName) > 200");
            Sql("update medias set Name = substring(Name, 0, 200) where len(Name) > 200");

            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medias", "OriginalFileName", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Medias", "Name", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
