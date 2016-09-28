using System;
using AutoMapper;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Models.Mappings.Converters
{
    /// <summary>
    /// ChartSettingConverter.
    /// </summary>
    /// <seealso cref="AutoMapper.ITypeConverter{ChartSetting, ChartSettingViewModel}" />
    public class ChartSettingConverter : ITypeConverter<ChartSetting, ChartSettingViewModel>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        /// <exception cref="System.ArgumentException">context.SourceValue</exception>
        public ChartSettingViewModel Convert(ResolutionContext context)
        {
            ChartSetting model = context.SourceValue as ChartSetting;

            if (model == null)
            {
                throw new ArgumentException("context.SourceValue");
            }

            if (model.Type == ChartType.Assessment)
            {
                return Mapper.Map<QuestionChartSettingViewModel>(model as QuestionChartSetting);
            }
            else
            {
                return Mapper.Map<VitalChartSettingViewModel>(model as VitalChartSetting);
            }
        }
    }
}