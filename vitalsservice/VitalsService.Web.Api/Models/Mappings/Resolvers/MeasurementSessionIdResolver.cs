using System;
using System.Linq;
using AutoMapper;
using VitalsService.Domain.DbEntities;

namespace VitalsService.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Resolves mapping for health session id in measurement.
    /// </summary>
    public class MeasurementSessionIdResolver : ValueResolver<Measurement, Guid?>
    {
        protected override Guid? ResolveCore(Measurement source)
        {
            if (source.MeasurementValues == null || !source.MeasurementValues.Any())
            {
                return null;
            }

            return source.MeasurementValues.First().HealthSessionElement.HealthSessionId;
        }
    }
}