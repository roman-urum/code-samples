using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    /// <summary>
    /// CustomerIdIndexesAdded.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class CustomerIdIndexesAdded : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateIndex("dbo.Alerts", "PatientId", name: "IX_PATIENT_ID");
            CreateIndex("dbo.Alerts", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.AlertSeverities", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Thresholds", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Measurements", "PatientId", name: "IX_PATIENT_ID");
            CreateIndex("dbo.Measurements", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.HealthSessions", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.HealthSessions", "PatientId", name: "IX_PATIENT_ID");
            CreateIndex("dbo.Notes", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.Notes", "PatientId", name: "IX_PATIENT_ID");
            CreateIndex("dbo.AssessmentMedias", "CustomerId", name: "IX_CUSTOMER_ID");
            CreateIndex("dbo.AssessmentMedias", "PatientId", name: "IX_PATIENT_ID");
            CreateIndex("dbo.PatientThresholds", "PatientId", name: "IX_PATIENT_ID");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropIndex("dbo.PatientThresholds", "IX_PATIENT_ID");
            DropIndex("dbo.AssessmentMedias", "IX_PATIENT_ID");
            DropIndex("dbo.AssessmentMedias", "IX_CUSTOMER_ID");
            DropIndex("dbo.Notes", "IX_PATIENT_ID");
            DropIndex("dbo.Notes", "IX_CUSTOMER_ID");
            DropIndex("dbo.HealthSessions", "IX_PATIENT_ID");
            DropIndex("dbo.HealthSessions", "IX_CUSTOMER_ID");
            DropIndex("dbo.Measurements", "IX_CUSTOMER_ID");
            DropIndex("dbo.Measurements", "IX_PATIENT_ID");
            DropIndex("dbo.Thresholds", "IX_CUSTOMER_ID");
            DropIndex("dbo.AlertSeverities", "IX_CUSTOMER_ID");
            DropIndex("dbo.Alerts", "IX_CUSTOMER_ID");
            DropIndex("dbo.Alerts", "IX_PATIENT_ID");
        }
    }
}