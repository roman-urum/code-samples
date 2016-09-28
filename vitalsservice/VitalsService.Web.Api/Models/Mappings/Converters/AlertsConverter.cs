using System;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.Alerts;
using VitalsService.Web.Api.Models.AlertSeverities;

namespace VitalsService.Web.Api.Models.Mappings.Converters
{
    /// <summary>
    /// Contains logic to build alert dtos base on alert type and search criteria.
    /// </summary>
    public class AlertsConverter : ITypeConverter<Alert, BaseAlertResponseDto>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public BaseAlertResponseDto Convert(ResolutionContext context)
        {
            Alert alert = null;

            if (context != null && context.SourceValue != null)
            {
                alert = context.SourceValue as Alert;
            }

            if (alert == null)
            {
                throw new ArgumentException("context.SourceValue");
            }

            var options = context.Options.Items;
            object isBrief;

            if (options.TryGetValue("isBrief", out isBrief) && !(bool) isBrief)
            {
                switch (alert.Type)
                {
                    case AlertType.VitalsViolation:
                        var vitalAlert = alert as VitalAlert;

                        if (vitalAlert != null)
                        {
                            return Mapper.Map<VitalAlertResponseDto>(vitalAlert);
                        }

                        break;

                    case AlertType.ResponseViolation:
                        var healthSessionElementAlert = alert as HealthSessionElementAlert;

                        if (healthSessionElementAlert != null)
                        {
                            return Mapper.Map<HealthSessionElementAlertResponseDto>(healthSessionElementAlert);
                        }

                        break;
                }
            }

            return new BaseAlertResponseDto
            {
                Acknowledged = alert.Acknowledged,
                Type = alert.Type.Value,
                CustomerId = alert.CustomerId,
                Id = alert.Id,
                AlertSeverity = Mapper.Map<AlertSeverityResponseDto>(alert.AlertSeverity),
                AcknowledgedBy = alert.AcknowledgedBy,
                AcknowledgedUtc = alert.AcknowledgedUtc,
                Body = alert.Body,
                ExpiresUtc = alert.ExpiresUtc,
                OccurredUtc = alert.OccurredUtc,
                Title = alert.Title,
                Weight = alert.Weight
            };
        }
    }
}