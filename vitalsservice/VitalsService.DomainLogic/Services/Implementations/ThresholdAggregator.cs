using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// ThresholdAggregator.
    /// </summary>
    public class ThresholdAggregator : IThresholdAggregator
    {
        /// <summary>
        /// Aggregates the thresholds.
        /// </summary>
        /// <param name="defaultThresholds">The default thresholds.</param>
        /// <param name="patientsThresholds">The patients thresholds.</param>
        /// <returns></returns>
        public IList<Threshold> AggregateThresholds(
            IList<DefaultThreshold> defaultThresholds,
            IList<PatientThreshold> patientsThresholds
        )
        {
            var allThresholds = Mapper.Map<IList<Threshold>>(defaultThresholds);
            allThresholds= allThresholds.Concat(Mapper.Map<IList<Threshold>>(patientsThresholds)).ToList();

            var groupedByNameAndSeverity = allThresholds
                .GroupBy(t => new { t.Name, t.AlertSeverityId, t.CustomerId })
                .ToList();

            var aggregatedThresholds = new List<Threshold>();

            foreach (var group in groupedByNameAndSeverity)
            {
                if (!group.Key.AlertSeverityId.HasValue && groupedByNameAndSeverity.Any(g => g.Key.AlertSeverityId.HasValue))
                {
                    continue;
                }

                if (group.Any(g => g is PatientThreshold))
                {
                    aggregatedThresholds.Add(group.Single(g => g is PatientThreshold));
                }
                else if (group.Any(g => g is DefaultThreshold))
                {
                    var conditionThresholdsInGroup = group
                        .OfType<DefaultThreshold>()
                        .Where(t => t.DefaultType.ToLower() == ThresholdDefaultType.Condition.ToString().ToLower());

                    if (conditionThresholdsInGroup.Any())
                    {
                        aggregatedThresholds.Add(new Threshold()
                        {
                            AlertSeverity = conditionThresholdsInGroup.First().AlertSeverity,
                            AlertSeverityId = conditionThresholdsInGroup.First().AlertSeverityId,
                            CustomerId = conditionThresholdsInGroup.First().CustomerId,
                            Name = conditionThresholdsInGroup.First().Name,
                            Type = conditionThresholdsInGroup.First().Type,
                            Unit = conditionThresholdsInGroup.First().Unit,
                            MaxValue = conditionThresholdsInGroup.Min(t => t.MaxValue),
                            MinValue = conditionThresholdsInGroup.Max(t => t.MinValue)
                        });
                    }
                    else
                    {
                        aggregatedThresholds.Add(group.Single(g => g is DefaultThreshold));
                    }                                  
                }
            }

            return aggregatedThresholds;
        }
    }
}