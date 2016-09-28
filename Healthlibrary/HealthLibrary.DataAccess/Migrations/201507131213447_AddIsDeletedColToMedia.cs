namespace HealthLibrary.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedColToMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medias", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medias", "IsDeleted");
        }
    }
}
