using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    /// <summary>
    /// PatientConditionsAdded.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class PatientConditionsAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.PatientConditions",
                c => new
                    {
                        PatientId = c.Guid(nullable: false),
                        ConditionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatientId, t.ConditionId })
                .ForeignKey("dbo.Conditions", t => t.ConditionId, cascadeDelete: true)
                .Index(t => t.ConditionId, name: "IX_PATIENT_CONDITION_ID");
            
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.PatientConditions", "ConditionId", "dbo.Conditions");
            DropIndex("dbo.PatientConditions", "IX_PATIENT_CONDITION_ID");
            DropTable("dbo.PatientConditions");
        }
    }
}