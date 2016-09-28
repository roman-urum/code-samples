using System.Data.Entity.Migrations;

namespace VitalsService.DataAccess.EF.Migrations
{
    /// <summary>
    /// TimezonesAddedToSeveralEntities.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class TimezonesAddedToSeveralEntities : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Measurements", "ObservedTz", c => c.String(maxLength: 44));
            AddColumn("dbo.HealthSessionElements", "AnsweredTz", c => c.String(maxLength: 44));
            AddColumn("dbo.HealthSessions", "ScheduledTz", c => c.String(maxLength: 44));
            AddColumn("dbo.HealthSessions", "StartedTz", c => c.String(maxLength: 44));
            AddColumn("dbo.HealthSessions", "CompletedTz", c => c.String(maxLength: 44));

            Sql("UPDATE dbo.Measurements SET ObservedTz='Etc/UTC'");
            Sql("UPDATE dbo.HealthSessionElements SET AnsweredTz='Etc/UTC'");
            Sql("UPDATE dbo.HealthSessions SET ScheduledTz='Etc/UTC',StartedTz='Etc/UTC',CompletedTz='Etc/UTC'");
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.HealthSessions", "CompletedTz");
            DropColumn("dbo.HealthSessions", "StartedTz");
            DropColumn("dbo.HealthSessions", "ScheduledTz");
            DropColumn("dbo.HealthSessionElements", "AnsweredTz");
            DropColumn("dbo.Measurements", "ObservedTz");
        }
    }
}