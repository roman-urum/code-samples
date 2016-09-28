using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Areas.Site.Models.Dashboard
{
    /// <summary>
    /// Help class to build model of patient card.
    /// </summary>
    public class PatientCardModelBuilder
    {
        private readonly IList<QuestionReadingViewModel> recentQuestionReadings;
        private readonly IList<VitalReadingViewModel> recentVitalReadings;
        private readonly IList<AdherenceDto> recentAdherences;
        private readonly IList<BaseAlertResponseDto> patientAlerts;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="recentQuestionReadings"></param>
        /// <param name="recentVitalReadings"></param>
        /// <param name="recentAdherences"></param>
        /// <param name="patientAlerts">List of ALL patient alerts.</param>
        public PatientCardModelBuilder(
            IList<QuestionReadingViewModel> recentQuestionReadings,
            IList<VitalReadingViewModel> recentVitalReadings,
            IList<AdherenceDto> recentAdherences,
            IList<BaseAlertResponseDto> patientAlerts)
        {
            this.recentAdherences = recentAdherences;
            this.recentQuestionReadings = recentQuestionReadings;
            this.recentVitalReadings = recentVitalReadings;
            this.patientAlerts = patientAlerts;
        }

        /// <summary>
        /// Creates new instance of view model for alert based on provided dto.
        /// </summary>
        /// <param name="alertDto"></param>
        /// <returns></returns>
        public FullPatientCardViewModel Build(BaseAlertResponseDto alertDto)
        {
            var recentReadings = this.BuildReadingsList(alertDto);

            var fullPatientCard = new FullPatientCardViewModel
            {
                Id = alertDto.Id,
                Reading = Mapper.Map<BaseReadingViewModel>(alertDto),
                RecentReadings = recentReadings.OrderByDescending(r => r.Date).Take(6).ToList(),
                TotalRecentReadingsCount = recentReadings.Count
            };

            var vitalAlertDto = alertDto as VitalAlertResponseDto;

            if (vitalAlertDto != null)
            {
                fullPatientCard.Threshold =
                    Mapper.Map<ThresholdViewModel>(vitalAlertDto.ViolatedThreshold);
            }

            var vitalReading = fullPatientCard.Reading as VitalReadingViewModel;

            if (vitalReading != null)
            {
                fullPatientCard.Reading = this.UpdateAlertDetails(vitalReading);
            }

            return fullPatientCard;
        }

        /// <summary>
        /// Updates color codes and severity for vitals in provided reading.
        /// </summary>
        /// <param name="reading"></param>
        /// <returns></returns>
        private VitalReadingViewModel UpdateAlertDetails(VitalReadingViewModel reading)
        {
            foreach (var dashboardVitalViewModel in reading.Measurement.Vitals)
            {
                var vitalAlert = this.patientAlerts.FirstOrDefault(a => a.Id == dashboardVitalViewModel.Id);

                if (vitalAlert != null)
                {
                    dashboardVitalViewModel.VitalAlert = Mapper.Map<AlertViewModel>(vitalAlert);
                }
            }

            return reading;
        }

        /// <summary>
        /// Creates list of recent readings for specified alert dto.
        /// </summary>
        /// <param name="alertDto"></param>
        /// <returns></returns>
        private IList<BaseReadingViewModel> BuildReadingsList(BaseAlertResponseDto alertDto)
        {
            var recentReadings = new List<BaseReadingViewModel>();
            var vitalAlertDetails = alertDto as VitalAlertResponseDto;

            if (vitalAlertDetails != null)
            {
                var vitalAlertObserved = vitalAlertDetails.Measurement.ObservedUtc
                    .ConvertTimeFromUtc(vitalAlertDetails.Measurement.ObservedTz);

                recentReadings.AddRange(
                    recentVitalReadings.Where(r =>
                        r.Name == vitalAlertDetails.Name.ToString() &&
                        r.Date <= vitalAlertObserved
                    )
                );
            }

            var healthSessionElementAlertDetails = alertDto as HealthSessionElementAlertResponseDto;

            if (healthSessionElementAlertDetails != null)
            {
                var healthSessionElementDate = healthSessionElementAlertDetails.AnsweredUtc.ConvertTimeFromUtc(healthSessionElementAlertDetails.AnsweredTz);
                recentReadings.AddRange(
                    recentQuestionReadings.Where(r =>
                        r.ElementId == healthSessionElementAlertDetails.ElementId &&
                        r.Date <= healthSessionElementDate
                    )
                );
            }

            if (alertDto.Type == AlertType.Adherence)
            {
                recentReadings.AddRange(Mapper.Map<IList<AdherenceReadingViewModel>>(recentAdherences));
            }

            return recentReadings;
        }
    }
}
