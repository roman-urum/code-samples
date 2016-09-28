namespace VitalsService.DataAccess.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssessmentMedia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssessmentMedias",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        OriginalFileName = c.String(nullable: false, maxLength: 100),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        ContentLength = c.Long(nullable: false),
                        MediaType = c.String(nullable: false, maxLength: 20),
                        StorageKey = c.String(maxLength: 1000),
                        CreatedUtc = c.DateTime(nullable: false),
                        UpdatedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AssessmentMedias");
        }
    }
}
