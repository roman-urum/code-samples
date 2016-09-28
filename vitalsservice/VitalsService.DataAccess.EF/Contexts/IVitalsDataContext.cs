using System.Data.Entity;
using VitalsService.Domain.DbEntities;

namespace VitalsService.DataAccess.EF.Contexts
{
    public interface IVitalsDataContext
    {
        DbSet<MeasurementNote> MeasurementNotes { get; set; }

        DbSet<Vital> Vitals { get; set; }

        DbSet<Measurement> Measurements { get; set; }

        DbSet<HealthSession> HealthSessions { get; set; }

        DbSet<HealthSessionElement> HealthSessionElements { get; set; }

        DbSet<HealthSessionElementValue> HealthSessionElementValues { get; set; }

        DbSet<Threshold> Thresholds { get; set; }

        DbSet<AlertSeverity> AlertSeverities { get; set; }
        
        DbSet<VitalAlert> VitalAlerts { get; set; }

        DbSet<HealthSessionElementAlert> HealthSessionElementAlerts { get; set; }

        DbSet<AssessmentMedia> AssessmentMedias { get; set; }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        DbSet<Condition> Conditions { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        DbSet<Tag> Tags { get; set; }
    }
}
