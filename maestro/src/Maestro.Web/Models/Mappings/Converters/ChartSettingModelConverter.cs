using System;
using AutoMapper;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// Converts models of charts settings to appropriate type of settings entity.
    /// </summary>
    public class ChartSettingModelConverter : ITypeConverter<ChartSettingViewModel, ChartSetting>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        /// <exception cref="System.ArgumentException">context.SourceValue</exception>
        public ChartSetting Convert(ResolutionContext context)
        {
            ChartSettingViewModel model = context.SourceValue as ChartSettingViewModel;

            if (model == null)
            {
                throw new ArgumentException("context.SourceValue");
            }

            if (model.Type == ChartType.Assessment)
            {
                return Mapper.Map<QuestionChartSetting>(model as QuestionChartSettingViewModel);
            }
            else
            {
                return Mapper.Map<VitalChartSetting>(model as VitalChartSettingViewModel);
            }
        }
    }
}