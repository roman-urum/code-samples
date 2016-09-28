using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// BriefPatientCardViewModel.
    /// </summary>
    public class BriefPatientCardViewModel
    {
        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>
        /// The alerts.
        /// </value>
        public AlertSeverityViewModel SeverityOfLatestAlert { get; set; }

        /// <summary>
        /// Gets or sets the patient information.
        /// </summary>
        /// <value>
        /// The patient information.
        /// </value>
        public PatientInfoViewModel PatientInfo { get; set; }

        /// <summary>
        /// Gets or sets the list of alert counts.
        /// </summary>
        /// <value>
        /// The list of alert counts.
        /// </value>
        public IList<AlertCountViewModel> Counts { get; set; }

        /// <summary>
        /// Gets or the most recent date of highest severity.
        /// </summary>
        /// Note: This property is being used only for sorting on backend,
        /// so it isn't necessary to convert it to local timezone
        public DateTime GetMostRecentDateOfHighestSeverityUtc()
        {
                return Counts.Max(c => c.MostRecentDateOfHighestSeverityUtc);
        }
        
        [JsonIgnore]
        public IList<Tuple<int, int>> VitalsAlertNumbers { get; set; }

        [JsonIgnore]
        public IList<Tuple<int, int>> ResponseAlertNumbers { get; set; }

        [JsonIgnore]
        public IList<Tuple<int, int>> AdherenceAlertNumbers { get; set; }

        public int GetSeverityIndicator()
        {
            var maxMeasurementSeverity = this
                .Counts
                .Where(c => c.AlertType == AlertType.VitalsViolation)
                .SelectMany(c => c.AlertSeverityCounts)
                .Where(c => c.Count > 0)
                .OrderByDescending(c => c.Severity)
                .Select(c => new Tuple<int, AlertType>(c.Severity, AlertType.VitalsViolation))
                .FirstOrDefault();

            var maxBehaviorSeverity = this
                .Counts
                .Where(c => c.AlertType == AlertType.ResponseViolation)
                .SelectMany(c => c.AlertSeverityCounts)
                .Where(c => c.Count > 0)
                .OrderByDescending(c => c.Severity)
                .Select(c => new Tuple<int, AlertType>(c.Severity, AlertType.ResponseViolation))
                .FirstOrDefault();

            var maxAdherenceSeverity = this
                .Counts
                .Where(c => c.AlertType == AlertType.Adherence)
                .SelectMany(c => c.AlertSeverityCounts)
                .Where(c => c.Count > 0)
                .OrderByDescending(c => c.Severity)
                .Select(c => new Tuple<int, AlertType>(c.Severity, AlertType.Adherence))
                .FirstOrDefault();

            var severityCounts = new List<Tuple<int, AlertType>>();
            if (maxMeasurementSeverity != null) severityCounts.Add(maxMeasurementSeverity);
            if (maxBehaviorSeverity != null) severityCounts.Add(maxBehaviorSeverity);
            if (maxAdherenceSeverity != null) severityCounts.Add(maxAdherenceSeverity);

            if (!severityCounts.Any()) return 0;

            var alertTypePriorities = new Dictionary<AlertType, int>()
            {
                { AlertType.VitalsViolation, 2 },
                { AlertType.ResponseViolation, 1 },
                { AlertType.Adherence, 0 },
                { AlertType.Insight, 0 }
            };

            var mostPriorittSeverityCount = severityCounts.OrderByDescending(c => c.Item1)
                .ThenByDescending(c => alertTypePriorities[c.Item2])
                .FirstOrDefault();

            if (mostPriorittSeverityCount == null) return 0;

            return 2 * mostPriorittSeverityCount.Item1 + alertTypePriorities[mostPriorittSeverityCount.Item2];

        }
    }

}