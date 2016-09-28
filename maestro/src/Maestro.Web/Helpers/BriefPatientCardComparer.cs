using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Web.Areas.Site.Models.Dashboard;

namespace Maestro.Web.Helpers
{
    public class BriefPatientCardComparer : IComparer<BriefPatientCardViewModel>
    {
        private IList<AlertSeverityResponseDto> severities;
        public BriefPatientCardComparer(IList<AlertSeverityResponseDto> severities)
        {
            this.severities = severities;
        }

        public int Compare(BriefPatientCardViewModel x, BriefPatientCardViewModel y)
        {
            if (x == null && y == null) return 0;

            if (x == null) return -1;

            if (y == null) return 1;

            int xSeverityIndicator = x.GetSeverityIndicator();
            int ySeverityIndicator = y.GetSeverityIndicator();

            if (xSeverityIndicator != ySeverityIndicator)
            {
                return xSeverityIndicator.CompareTo(ySeverityIndicator);
            }

            foreach (var severity in severities.OrderByDescending(s => s.Severity))
            {
                #region compare vitals alerts number
                var xSeverityCountInfo = x.VitalsAlertNumbers.FirstOrDefault(an => an.Item1 == severity.Severity);
                var ySeverityCountInfo = y.VitalsAlertNumbers.FirstOrDefault(an => an.Item1 == severity.Severity);

                var severityCountInfoCompareResult = CompareSeverityCountInfos(xSeverityCountInfo, ySeverityCountInfo);
                if (severityCountInfoCompareResult != 0) return severityCountInfoCompareResult;
                #endregion

                #region compare response alerts number
                xSeverityCountInfo = x.ResponseAlertNumbers.FirstOrDefault(an => an.Item1 == severity.Severity);
                ySeverityCountInfo = y.ResponseAlertNumbers.FirstOrDefault(an => an.Item1 == severity.Severity);

                severityCountInfoCompareResult = CompareSeverityCountInfos(xSeverityCountInfo, ySeverityCountInfo);
                if (severityCountInfoCompareResult != 0) return severityCountInfoCompareResult;
                #endregion

                #region compare adherence alerts number
                xSeverityCountInfo = x.AdherenceAlertNumbers.FirstOrDefault(an => an.Item1 == severity.Severity);
                ySeverityCountInfo = y.AdherenceAlertNumbers.FirstOrDefault(an => an.Item1 == severity.Severity);

                severityCountInfoCompareResult = CompareSeverityCountInfos(xSeverityCountInfo, ySeverityCountInfo);
                if (severityCountInfoCompareResult != 0) return severityCountInfoCompareResult;
                #endregion

            }

            if (x.GetMostRecentDateOfHighestSeverityUtc() != y.GetMostRecentDateOfHighestSeverityUtc())
            {
                return x.GetMostRecentDateOfHighestSeverityUtc().CompareTo(y.GetMostRecentDateOfHighestSeverityUtc());
            }

            return 0;
        }

        private int CompareSeverityCountInfos(Tuple<int, int> countInfo1, Tuple<int, int> countInfo2)
        {
            if (countInfo1 == null && countInfo2 == null) return 0;

            if (countInfo1 == null) return -1;
            if (countInfo2 == null) return 1;

            if (countInfo1.Item1 != countInfo2.Item1)
            {
                return countInfo1.Item1.CompareTo(countInfo2.Item1);
            }

            if (countInfo1.Item2 != countInfo2.Item2)
            {
                return countInfo1.Item2.CompareTo(countInfo2.Item2);
            }

            return 0;
        }
    }
}