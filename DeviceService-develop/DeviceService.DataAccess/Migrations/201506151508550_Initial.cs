using System.Data.Entity.Migrations;

namespace DeviceService.DataAccess.Migrations
{
    /// <summary>
    /// Initial.
    /// </summary>
    public partial class Initial : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceId = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ActivationCode = c.String(maxLength: 10),
                        BirthDate = c.DateTime(),
                        DeviceModel = c.String(maxLength: 100),
                        PublicKey = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropTable("dbo.Devices");
        }
    }
}