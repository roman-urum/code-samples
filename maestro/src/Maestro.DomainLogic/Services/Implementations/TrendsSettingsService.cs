using System;
using System.Linq;
using System.Threading.Tasks;
using Maestro.DataAccess.EF.DataAccess;
using Maestro.Domain.DbEntities;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Contains business logic for saving of trends settings in db.
    /// </summary>
    public class TrendsSettingsService : ITrendsSettingsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TrendSetting> trendsSettingsRepository;
        private readonly IRepository<QuestionChartSetting> questionChartSettingsRepository;
        private readonly IRepository<VitalChartSetting> vitalChartSettingRepository;
        private readonly IRepository<DisplayThresholdSetting> displayThresholdsSettingsRepository; 

        /// <summary>
        /// Initializes a new instance of the <see cref="TrendsSettingsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TrendsSettingsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.trendsSettingsRepository = this.unitOfWork.CreateGenericRepository<TrendSetting>();
            this.questionChartSettingsRepository = this.unitOfWork.CreateGenericRepository<QuestionChartSetting>();
            this.vitalChartSettingRepository = this.unitOfWork.CreateGenericRepository<VitalChartSetting>();
            this.displayThresholdsSettingsRepository =
                this.unitOfWork.CreateGenericRepository<DisplayThresholdSetting>();
        }

        /// <summary>
        /// Deletes all existing charts settings for patient with the same id and create provided entity.
        /// </summary>
        /// <param name="trendSetting"></param>
        /// <returns></returns>
        public async Task Save(TrendSetting trendSetting)
        {
            var savedTrendsSettings =
                await this.trendsSettingsRepository.FindAsync(t => t.PatientId == trendSetting.PatientId);

            foreach (var savedSetting in savedTrendsSettings)
            {
                this.DeleteTrendSetting(savedSetting);
            }

            var i = 0;

            foreach (var chartSetting in trendSetting.ChartsSettings)
            {
                chartSetting.Order = i;
                i++;
            }

            this.trendsSettingsRepository.Insert(trendSetting);
            await this.unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Returns charts settings for specified patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<TrendSetting> GetByPatientId(Guid patientId)
        {
            var settings = await this.trendsSettingsRepository.FindAsync(t => t.PatientId == patientId);
            var result = settings.FirstOrDefault();

            if (result != null)
            {
                result.ChartsSettings = result.ChartsSettings.OrderBy(s => s.Order).ToList();
            }

            return result;
        }

        /// <summary>
        /// Deletes trend setting from repository without saving changes in db context.
        /// </summary>
        /// <param name="trendSetting"></param>
        private void DeleteTrendSetting(TrendSetting trendSetting)
        {
            var chartsSettings = trendSetting.ChartsSettings.ToList();

            foreach (var chartsSetting in chartsSettings)
            {
                var questionSetting = chartsSetting as QuestionChartSetting;

                if (questionSetting != null)
                {
                    this.questionChartSettingsRepository.Delete(questionSetting);
                }

                var vitalSetting = chartsSetting as VitalChartSetting;

                if (vitalSetting != null)
                {
                    this.displayThresholdsSettingsRepository.DeleteRange(vitalSetting.DisplayThresholds);
                    this.vitalChartSettingRepository.Delete(vitalSetting);
                }
            }

            this.trendsSettingsRepository.Delete(trendSetting);
        }
    }
}
