using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Maestro.Common.Extensions;
using Maestro.Common.Helpers;
using Maestro.Domain;
using Maestro.Domain.DbEntities;
using Maestro.Domain.Dtos.DevicesService;
using Maestro.Domain.Dtos.HealthLibraryService;
using Maestro.Domain.Dtos.PatientsService;
using Maestro.Domain.Dtos.PatientsService.Calendar;
using Maestro.Domain.Dtos.PatientsService.DefaultSessions;
using Maestro.Domain.Dtos.PatientsService.Enums.Ordering;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.PatientNotes;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Domain.Enums;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Reporting.Models.PatientTrends;
using Maestro.Web.Areas.Customer.Models.CareBuilder;
using Maestro.Web.Areas.Site;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Areas.Site.Models.Dashboard;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Calendar;
using Maestro.Web.Areas.Site.Models.Patients.Charts;
using Maestro.Web.Areas.Site.Models.Patients.Dashboard;
using Maestro.Web.Areas.Site.Models.Patients.DetailedData;
using Maestro.Web.Areas.Site.Models.Patients.Notes;
using Maestro.Web.Areas.Site.Models.Patients.SearchPatients;
using Maestro.Web.Models.Mappings.Converters;
using Maestro.Web.Resources;
using Microsoft.Practices.ServiceLocation;

namespace Maestro.Web.Models.Mappings
{
    /// <summary>
    /// PatientMappings.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class PatientMappings : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            #region Patient's common

            CreateMap<PatientDto, BriefPatientViewModel>();

            CreateMap<PatientDto, FullPatientViewModel>()
                .ForMember(d => d.CareManagers, m => m.Ignore());

            CreateMap<PatientDto, UpdatePatientRequestDto>();

            CreateMap<HealthSessionElementDto, HealthSessionDetailedDataGroupElementViewModel>()
                .ForMember(d => d.Answered, o => o.MapFrom(s => s.AnsweredUtc.HasValue ? s.AnsweredUtc.Value.ConvertTimeFromUtc(s.AnsweredTz) : (DateTimeOffset?)null));

            CreateMap<HealthSessionElementValueDto, HealthSessionElementValueViewModel>().ConvertUsing<HealthSessionElementValueConverter>();

            CreateMap<MeasurementValueResponseDto, MeasurementHealthSessionElementValueViewModel>();

            CreateMap<DeviceDto, MeasurementDeviceDto>();

            CreateMap<SelectionAnswerDto, SelectionAnswerHealthSessionElementValueViewModel>();

            CreateMap<ScaleAndFreeFormAnswerDto, ScaleAndFreeFormAnswerHealthSessionElementValueViewModel>();

            CreateMap<MeasurementBriefResponseDto, MeasurementBriefViewModel>()
                .ForMember(d => d.Created, o => o.MapFrom(s => s.CreatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime))
                .ForMember(d => d.Updated, o => o.MapFrom(s => s.UpdatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime))
                .ForMember(d => d.Observed, o => o.MapFrom(s => s.ObservedUtc.ConvertTimeFromUtc(s.ObservedTz)));

            CreateMap<MeasurementBriefResponseDto, MeasurementViewModel>().IncludeBase<MeasurementBriefResponseDto, MeasurementBriefViewModel>();

            CreateMap<MeasurementDto, MeasurementViewModel>()
                .IncludeBase<MeasurementBriefResponseDto, MeasurementBriefViewModel>()
                .ForMember(d => d.Created, o => o.MapFrom(s => s.CreatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime))
                .ForMember(d => d.Updated, o => o.MapFrom(s => s.UpdatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime))
                .ForMember(d => d.Observed, o => o.MapFrom(s => s.ObservedUtc.ConvertTimeFromUtc(s.ObservedTz))); ;

            CreateMap<AssessmentValueResponseDto, AssessmentHealthSessionElementValueViewModel>();

            CreateMap<AssessmentMediaResponseDto, AssessmentMediaViewModel>()
                .ForMember(d => d.Created, o => o.MapFrom(s => s.CreatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime))
                .ForMember(d => d.Updated, o => o.MapFrom(s => s.UpdatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime));

            CreateMap<ScheduleCalendarProgramViewModel, ProgramScheduleRequestDto>()
                .ForMember(d => d.StartDateUtc, m => m.MapFrom(s => s.StartDate.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone)))
                .ForMember(d => d.StartDateTz, m => m.MapFrom(s => PatientContext.Current.Patient.TimeZone));

            CreateMap<AdherenceDto, AdherenceViewModel>()
                .ForMember(d => d.Scheduled, m => m.MapFrom(s => s.ScheduledUtc.ConvertTimeFromUtc(s.EventTz)))
                .ForMember(
                    d => d.Expiration,
                    m => m.MapFrom(
                        s => s.ExpirationUtc.HasValue ?
                        s.ExpirationUtc.Value.ConvertTimeFromUtc(s.EventTz) :
                        (DateTimeOffset?)null
                    )
                )
                .ForMember(d => d.Updated, m => m.MapFrom(s => s.UpdatedUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime));

            CreateMap<CalendarItemResponseDto, CalendarItemViewModel>()
                .ForMember(
                    d => d.Due, m => m.MapFrom(s => s.DueUtc.HasValue ?
                        s.DueUtc.Value.ConvertTimeFromUtc(s.EventTz) :
                        (DateTimeOffset?)null
                    )
                );

            CreateMap<CalendarProgramResponseDto, CalendarProgramViewModel>()
                .ForMember(d => d.StartDate, m => m.MapFrom(s => s.StartDateUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime));

            CreateMap<CalendarChangeResponseDto, CalendarChangeViewModel>()
                .ConvertUsing<CalendarChangesConverter>();

            CreateMap<CalendarItemChangeResponseDto, CalendarItemChangeViewModel>()
                .ForMember(
                    d => d.ChangedBy, m => m.MapFrom(
                        s => string.IsNullOrEmpty(s.ChangedBy) ?
                        GlobalStrings.Patient_CalendarHistory_UnknownAccountLabel :
                        s.ChangedBy
                    )
                )
                .ForMember(
                    d => d.DueDate, m => m.MapFrom(
                        s => s.DueUtc.HasValue ?
                        s.DueUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                )
                .ForMember(
                    d => d.PrevDueDate, m => m.MapFrom(
                        s => s.PrevDueUtc.HasValue ?
                        s.PrevDueUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                )
                .ForMember(
                    d => d.RecurrenceStartDate, m => m.MapFrom(
                        s => s.RecurrenceStartDateUtc.HasValue ?
                        s.RecurrenceStartDateUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                )
                .ForMember(
                    d => d.RecurrenceEndDate, m => m.MapFrom(
                        s => s.RecurrenceEndDateUtc.HasValue ?
                        s.RecurrenceEndDateUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                )
                .ForMember(
                    d => d.PrevRecurrenceStartDate, m => m.MapFrom(
                        s => s.PrevRecurrenceStartDateUtc.HasValue ?
                        s.PrevRecurrenceStartDateUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                )
                .ForMember(
                    d => d.PrevRecurrenceEndDate, m => m.MapFrom(
                        s => s.PrevRecurrenceEndDateUtc.HasValue ?
                        s.PrevRecurrenceEndDateUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                );

            CreateMap<CalendarProgramChangeResponseDto, CalendarProgramChangeViewModel>()
                .ForMember(
                    d => d.ChangedBy, m => m.MapFrom(
                        s => string.IsNullOrEmpty(s.ChangedBy)
                        ? GlobalStrings.Patient_CalendarHistory_UnknownAccountLabel
                        : s.ChangedBy))
                .ForMember(d => d.StartDate, m => m.MapFrom(s => s.StartDateUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime))
                .ForMember(
                    d => d.TerminationDate, m => m.MapFrom(
                        s => s.TerminationUtc.HasValue ?
                        s.TerminationUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime :
                        (DateTime?)null
                    )
                );

            CreateMap<DefaultSessionChangeResponseDto, DefaultSessionChangeViewModel>()
                .ForMember(
                    d => d.ChangedBy, m => m.MapFrom(
                        s => string.IsNullOrEmpty(s.ChangedBy)
                            ? GlobalStrings.Patient_CalendarHistory_UnknownAccountLabel
                            : s.ChangedBy));

            CreateMap<PatientDto, PatientInfoViewModel>()
                .ForMember(d => d.Phone, m => m.MapFrom(s => string.IsNullOrEmpty(s.PhoneWork) ? s.PhoneHome : s.PhoneWork))
                .ForMember(d => d.Name, m => m.MapFrom(s => string.Format("{0} {1}", s.FirstName, s.LastName)));

            CreateMap<PatientDto, SuggestionSearchPatientResultViewModel>();

            CreateMap<CalendarItemViewModel, CalendarItemRequestDto>()
                .ForMember(d => d.EventTz, m => m.MapFrom(s => PatientContext.Current.Patient.TimeZone))
                .ForMember(d => d.DueUtc, m => m.MapFrom(s => s.Due.HasValue ? s.Due.Value.DateTime.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone) : (DateTime?)null));

            CreateMap<ProtocolElementViewModel, ProtocolElementDto>();
            CreateMap<ProtocolElementDto, ProtocolElementViewModel>();

            CreateMap<RecurrenceRuleViewModel, RecurrenceRuleDto>()
                .ForMember(d => d.StartDateUtc, m => m.MapFrom(s => s.StartDate.HasValue ? s.StartDate.Value.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone) : (DateTime?)null))
                .ForMember(d => d.EndDateUtc, m => m.MapFrom(s => s.EndDate.HasValue ? s.EndDate.Value.ConvertTimeToUtc(PatientContext.Current.Patient.TimeZone) : (DateTime?)null));

            CreateMap<RecurrenceRuleDto, RecurrenceRuleViewModel>()
                .ForMember(d => d.StartDate, m => m.MapFrom(s => s.StartDateUtc.HasValue ? s.StartDateUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime: (DateTime?)null))
                .ForMember(d => d.EndDate, m => m.MapFrom(s => s.EndDateUtc.HasValue ? s.EndDateUtc.Value.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone).DateTime : (DateTime?)null ));

            #endregion

            #region Patient's vitals

            CreateMap<SearchVitalViewModel, SearchVitalsDto>();

            CreateMap<SearchAdhocMeasurementsDetailedDataViewModel, SearchVitalsDto>()
                .ForMember(d => d.ObservedFrom, m => m.MapFrom(s => s.ObservedFromUtc))
                .ForMember(d => d.ObservedTo, m => m.MapFrom(s => s.ObservedToUtc));

            CreateMap<SearchDetailedDataViewModel, SearchHealthSessionsDto>()
                .ForMember(d => d.StartedFromUtc, m => m.MapFrom(s => s.ObservedFromUtc))
                .ForMember(d => d.CompletedToUtc, m => m.MapFrom(s => s.ObservedToUtc));

            CreateMap<SearchUngroupedHealthSessionDetailedDataViewModel, SearchHealthSessionsDto>()
                .ForMember(d => d.StartedFromUtc, m => m.MapFrom(s => s.ObservedFromUtc))
                .ForMember(d => d.CompletedToUtc, m => m.MapFrom(s => s.ObservedToUtc));

            CreateMap<BaseAlertResponseDto, AlertViewModel>()
                .ForMember(d => d.Occurred, m => m.MapFrom(s => s.OccurredUtc.ConvertTimeFromUtc(PatientContext.Current.Patient.TimeZone)));

            CreateMap<VitalAlertBriefResponseDto, AlertViewModel>().IncludeBase<BaseAlertResponseDto, AlertViewModel>();

            CreateMap<VitalAlertResponseDto, AlertViewModel>()
                .ForMember(d => d.Occurred, m => m.MapFrom(s => s.Measurement.ObservedUtc.ConvertTimeFromUtc(s.Measurement.ObservedTz)));

            CreateMap<AlertSeverityResponseDto, AlertSeverityViewModel>();

            CreateMap<AlertSeverityViewModel, AlertSeverityCountViewModel>()
                .ForMember(d => d.Count, o => o.Ignore());

            CreateMap<VitalDto, VitalViewModel>()
                .BeforeMap((src, dest) =>
                {
                    var vitalConverter = ServiceLocator.Current.GetInstance<IVitalConverter>();

                    vitalConverter.Convert(src, new VitalSettings());
                })
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Description()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.Description()));

            CreateMap<VitalResponseDto, VitalViewModel>().IncludeBase<VitalDto, VitalViewModel>();

            CreateMap<VitalBriefResponseDto, VitalViewModel>().IncludeBase<VitalDto, VitalViewModel>();

            CreateMap<BaseAlertResponseDto, BaseReadingViewModel>().ConvertUsing<BaseReadingConverter>();

            CreateMap<VitalBriefResponseDto, VitalReadingViewModel>()
                .BeforeMap((src, dest) =>
                {
                    var vitalConverter = ServiceLocator.Current.GetInstance<IVitalConverter>();

                    vitalConverter.Convert(src, new VitalSettings());
                })
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.ToString()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.Description()))
                .ForMember(d => d.Date, m => m.Ignore())
                .ForMember(d => d.Measurement, m => m.Ignore())
                .ForMember(d => d.Alert, m => m.MapFrom(s => s.VitalAlert));

            CreateMap<VitalAlertResponseDto, VitalReadingViewModel>()
                .BeforeMap((src, dest) =>
                {
                    var vitalConverter = ServiceLocator.Current.GetInstance<IVitalConverter>();

                    vitalConverter.Convert(src, new VitalSettings());
                })
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Measurement.ObservedUtc.ConvertTimeFromUtc(s.Measurement.ObservedTz)))
                .ForMember(d => d.Alert, o => o.MapFrom(s => s));

            CreateMap<VitalAlertResponseDto, VitalAlertBriefResponseDto>();

            CreateMap<HealthSessionElementAlertResponseDto, QuestionReadingViewModel>()
                .ForMember(d => d.Date, o => o.MapFrom(s => s.AnsweredUtc.ConvertTimeFromUtc(s.AnsweredTz)))
                .ForMember(d => d.Alert, o => o.MapFrom(s => s));

            CreateMap<ViolatedThresholdDto, ThresholdViewModel>();

            CreateMap<VitalDto, VitalReadingViewModel>()
                .BeforeMap((src, dest) =>
                {
                    var vitalConverter = ServiceLocator.Current.GetInstance<IVitalConverter>();

                    vitalConverter.Convert(src, new VitalSettings());
                })
                .ForMember(d => d.Date, o => o.Ignore())
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Description()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.Description()));

            CreateMap<VitalDto, VitalReadingWithTrendViewModel>()
                .BeforeMap((src, dest) =>
                {
                    var vitalConverter = ServiceLocator.Current.GetInstance<IVitalConverter>();

                    vitalConverter.Convert(src, new VitalSettings());
                })
                .ForMember(d => d.Date, o => o.Ignore())
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Description()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.Description()));

            CreateMap<VitalBriefResponseDto, VitalReadingWithTrendViewModel>().BeforeMap(
                (src, dest) =>
                    {
                        var vitalConverter = ServiceLocator.Current.GetInstance<IVitalConverter>();

                        vitalConverter.Convert(src, new VitalSettings());
                    })
                .ForMember(d => d.Date, o => o.Ignore())
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Description()))
                .ForMember(d => d.Unit, m => m.MapFrom(s => s.Unit.Description()))
                .ForMember(d => d.Alert, m => m.MapFrom(s => s.VitalAlert));

            CreateMap<VitalDto, VitalBriefResponseDto>();

            CreateMap<HealthSessionElementDto, QuestionReadingViewModel>().ConvertUsing<QuestionReadingConverter>();

            CreateMap<IdentifierDto, IdentifierViewModel>()
                .ForMember(d => d.ValidationErrorMessage, m => m.ResolveUsing(src =>
                {
                    if (string.IsNullOrEmpty(src.ValidationRegEx))
                    {
                        return string.Empty;
                    }

                    if (!string.IsNullOrEmpty(src.ValidationErrorResourceString))
                    {
                        var resourceManager = GlobalStrings.ResourceManager;

                        var resourceString = resourceManager.GetString(src.ValidationErrorResourceString);

                        if (!string.IsNullOrEmpty(resourceString))
                        {
                            return resourceString;
                        }
                    }

                    return string.Format(GlobalStrings.PatientIdentifierDefaultValidationErrorMessage, src.Name);
                }));

            CreateMap<ThresholdDto, ThresholdViewModel>()
                .ForMember(d => d.IsDefault, o => o.UseValue(false));

            CreateMap<DefaultThresholdDto, ThresholdViewModel>()
                .ForMember(d => d.IsDefault, o => o.UseValue(true));

            CreateMap<SearchPatientsViewModel, PatientsSearchDto>()
                .ForMember(d => d.CustomerId, o => o.Ignore())
                .ForMember(d => d.SiteId, o => o.Ignore())
                .ForMember(d => d.IsBrief, o => o.UseValue(false))
                .ForMember(d => d.OrderBy, o => o.UseValue(PatientOrderBy.FullName))
                .ForMember(d => d.SortDirection, o => o.UseValue(SortDirection.Ascending));

            CreateMap<CalendarProgramResponseDto, PatientProgramViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.ProgramName))
                .ForMember(d => d.TotalDays, o => o.MapFrom(s => s.EndDay))
                .ForMember(d => d.CurrentDay, o => o.MapFrom(s => (DateTime.UtcNow - s.StartDateUtc).Days + s.StartDay));

            CreateMap<SearchNotesViewModel, SearchNotesDto>()
                .ForMember(d => d.CustomerId, o => o.Ignore())
                .ForMember(d => d.IsBrief, o => o.UseValue(true));

            CreateMap<CreateNoteViewModel, BaseNoteDto>()
                .ForMember(d => d.CreatedBy, o => o.Ignore());

            CreateMap<CreateNoteViewModel, CreateNoteRequestDto>()
                .ForMember(d => d.CreatedBy, o => o.Ignore());

            CreateMap<BaseNoteResponseDto, BaseNoteResponseViewModel>().ConvertUsing<NoteConverter>();

            CreateMap<NoteBriefResponseDto, NoteBriefResponseViewModel>();

            CreateMap<NoteDetailedResponseDto, NoteDetailedResponseViewModel>()
                .ForMember(d => d.MeasurementReading, o => o.MapFrom(s => s.Measurement))
                .ForMember(d => d.HealthSessionElementReading, o => o.MapFrom(s => s.HealthSessionElement));

            CreateMap<SearchProgramResultDto, SearchProgramResponseViewModel>();

            CreateMap<AdherenceDto, AdherenceReadingViewModel>()
                .ForMember(d => d.Date, o => o.MapFrom(s => s.ScheduledUtc.ConvertTimeFromUtc(s.EventTz)))
                .ForMember(d => d.Alert, o => o.Ignore());

            CreateMap<HealthSessionElementDto, HealthSessionElementViewModel>()
                .ForMember(d => d.Answered, o => o.MapFrom(
                    s => s.AnsweredUtc.HasValue ?
                    s.AnsweredUtc.Value.ConvertTimeFromUtc(s.AnsweredTz) :
                    (DateTimeOffset?)null
                )
            );

            #endregion

            #region Patient's charts

            CreateMap<TrendsSettingsViewModel, TrendSetting>()
                .ForMember(d => d.ChartsSettings, m => m.MapFrom(s => s.Charts))
                .ForMember(d => d.StartDate, m => m.MapFrom(s => s.DateRange.StartDate))
                .ForMember(d => d.EndDate, m => m.MapFrom(s => s.DateRange.EndDate));

            CreateMap<ChartSettingViewModel, ChartSetting>()
                .ConvertUsing<ChartSettingModelConverter>();

            CreateMap<QuestionChartSettingViewModel, QuestionChartSetting>();

            CreateMap<VitalChartSettingViewModel, VitalChartSetting>()
                .ForMember(d => d.VitalName, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.DisplayThresholds, m => m.MapFrom(s => s.ShowThresholdIds));

            CreateMap<Guid, DisplayThresholdSetting>()
                .ConvertUsing(s => new DisplayThresholdSetting { ThresholdId = s });

            CreateMap<TrendSetting, TrendsSettingsViewModel>()
                .ForMember(d => d.Charts, m => m.MapFrom(s => s.ChartsSettings))
                .ForMember(d => d.DateRange, m =>
                {
                    m.Condition(s => s.StartDate.HasValue && s.EndDate.HasValue);
                    m.MapFrom(s => s);
                });

            CreateMap<TrendSetting, ChartDateRangeViewModel>();

            CreateMap<ChartSetting, ChartSettingViewModel>()
                .ConvertUsing<ChartSettingConverter>();

            CreateMap<QuestionChartSetting, QuestionChartSettingViewModel>();

            CreateMap<VitalChartSetting, VitalChartSettingViewModel>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.VitalName))
                .ForMember(d => d.ShowThresholdIds, m => m.MapFrom(s => s.DisplayThresholds.Select(t => t.ThresholdId)));

            CreateMap<AssessmentChartViewModel, QuestionAnswersModel>()
                .ForMember(d => d.Question, m => m.MapFrom(s => s.ChartName))
                .ForMember(d => d.Answers, m => m.ResolveUsing(src =>
                {
                    var result = new List<AnswerModel>();

                    foreach (var chartData in src.ChartData)
                    {
                        var data = chartData as AssessmentChartPointViewModel;

                        if (data != null)
                        {
                            var answers = new List<string>();

                            foreach (var value in data.Values.Where(v => v.Count > 0))
                            {
                                var dataValue = value as SelectionAnswerStatisticViewModel;

                                if (dataValue != null)
                                {
                                    answers.Add(src.Answers.First(a => a.AnswerId == dataValue.AnswerId).AnswerText);
                                }
                            }

                            result.Add(
                                new AnswerModel()
                                {
                                    Answered = data.Date,
                                    Text = string.Join(", ", answers.OrderBy(v => v, new NaturalSortComparer()))
                                }
                            );
                        }
                    }

                    return result.OrderByDescending(a => a.Answered);
                }));

            CreateMap<VitalBriefResponseDto, VitalResponseModel>();

            #endregion
        }
    }
}