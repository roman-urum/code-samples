using System;
using AutoMapper;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// BaseReadingConverter.
    /// </summary>
    public class BaseReadingConverter : ITypeConverter<BaseAlertResponseDto, BaseReadingViewModel>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public BaseReadingViewModel Convert(ResolutionContext context)
        {
            var vitalAlertResponseDto = context.SourceValue as VitalAlertResponseDto;

            if (vitalAlertResponseDto != null)
            {
                return Mapper.Map<VitalReadingViewModel>(vitalAlertResponseDto);
            }

            var healthSessionElementAlertResponseDto = context.SourceValue as HealthSessionElementAlertResponseDto;

            if (healthSessionElementAlertResponseDto != null)
            {
                return Mapper.Map<QuestionReadingViewModel>(healthSessionElementAlertResponseDto);
            }

            var alert = context.SourceValue as BaseAlertResponseDto;

            if (alert != null && alert.Type == AlertType.Adherence)
            {
                var adherenceReading = new BaseReadingViewModel()
                {
                    Alert = Mapper.Map<AlertViewModel>(alert),
                    Date = null,
                    Id = Guid.Empty
                };

                return adherenceReading;
            }

            return null;
        }
    }
}