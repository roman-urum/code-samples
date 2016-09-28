using AutoMapper;
using Maestro.Domain.Dtos.HealthLibraryService;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.AssessmentElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.MeasurementElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerChoices;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.TextMediaElements;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;
using Maestro.Web.Areas.Customer.Models.CareBuilder;
using Maestro.Web.Areas.Customer.Models.CareBuilder.AssessmentElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;
using Maestro.Web.Areas.Customer.Models.CareBuilder.MeasurementElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Medias;
using Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols;
using Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements;
using Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SearchAnswerSets;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerChoices;
using Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet;
using Maestro.Web.Areas.Customer.Models.CareBuilder.TextMediaElements;
using Maestro.Web.Helpers;
using Maestro.Web.Models.Mappings.Converters;

namespace Maestro.Web.Models.Mappings
{
    /// <summary>
    /// CareBuilderMappings.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CareBuilderMappings : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<SelectionAnswerChoiceResponseDto, SelectionAnswerChoiceElementResponseViewModel>();

            CreateMap<ScaleAnswerSetRequestViewModel, CreateScaleAnswerSetRequestDto>()
                .ForMember(dto => dto.Labels, m => m.MapFrom(r => new CreateScaleAnswerSetLabelsRequestDto()
                {
                    HighLabel = new CreateLocalizedStringRequestDto { Value = r.HighLabel },
                    LowLabel = new CreateLocalizedStringRequestDto { Value = r.LowLabel },
                    MidLabel = r.MidLabel == null ? null : new CreateLocalizedStringRequestDto { Value = r.MidLabel }
                }));

            CreateMap<ScaleAnswerSetUpdateRequestViewModel, UpdateScaleAnswerSetRequestDto>()
                .ForMember(dto => dto.Labels, m => m.MapFrom(r => new UpdateScaleAnswerSetRequestDto
                {
                    Labels = new UpdateScaleAnswerSetLabelsRequestDto()
                    {
                        HighLabel = new UpdateLocalizedStringRequestDto() { Value = r.HighLabel },
                        LowLabel = new UpdateLocalizedStringRequestDto { Value = r.LowLabel },
                        MidLabel = r.MidLabel == null ? null : new UpdateLocalizedStringRequestDto { Value = r.MidLabel }
                    }
                }));

            CreateMap<UpdateScaleAnswerSetRequestDto, UpdateScaleAnswerSetLabelsRequestDto>()
                .ForMember(d => d.LowLabel, m => m.MapFrom(s => s.Labels.LowLabel))
                .ForMember(d => d.MidLabel, m => m.MapFrom(s => s.Labels.MidLabel))
                .ForMember(d => d.HighLabel, m => m.MapFrom(s => s.Labels.HighLabel));

            CreateMap<ScaleAnswerSetResponseDto, SearchScaleAnswerSetResultViewModel>()
                .ForMember(d => d.LowLabel, m => m.MapFrom(s => s.LowLabel.Value))
                .ForMember(d => d.MidLabel, m => m.MapFrom(s => s.MidLabel.Value))
                .ForMember(d => d.HighLabel, m => m.MapFrom(s => s.HighLabel.Value));

            CreateMap<CreateQuestionElementViewModel, CreateQuestionElementRequestDto>()
                .ForMember(d => d.QuestionElementString, m => m.Ignore());

            CreateMap<AnswerChoiceIdsViewModel, AnswerChoiceIdDto>();

            CreateMap<AnswerChoiceIdDto, AnswerChoiceIdsViewModel>();

            CreateMap<ScaleAnswerSetResponseDto, ScaleAnswerSetResponseViewModel>()
                .ForMember(d => d.LowLabel, m => m.MapFrom(s => s.LowLabel))
                .ForMember(d => d.MidLabel, m => m.MapFrom(s => s.MidLabel))
                .ForMember(d => d.HighLabel, m => m.MapFrom(s => s.HighLabel));

            CreateMap<CreateScaleAnswerSetRequestDto, SearchScaleAnswerSetResultViewModel>();

            CreateMap<SelectionAnswerSetResponseDto, SelectionAnswerSetViewModel>();

            CreateMap<QuestionElementResponseDto, QuestionElementListItemViewModel>()
                .ForMember(d => d.QuestionElementString, m => m.MapFrom(s => s.QuestionElementString))
                .ForMember(d => d.AnswerSetId, m => m.MapFrom(s => s.AnswerSet.Id))
                .ForMember(d => d.AnswerSetType, m => m.MapFrom(s => s.AnswerSet.Type));

            CreateMap<UpdateQuestionElementViewModel, UpdateQuestionElementRequestDto>()
                .ForMember(d => d.QuestionElementString, m => m.Ignore());

            CreateMap<LocalizedStringResponseDto, BaseLocalizedStringViewModel>();

            CreateMap<LocalizedStringResponseDto, LocalizedStringViewModel>();

            CreateMap<LocalizedStringWithAudioFileMediaResponseDto, LocalizedStringViewModel>();

            CreateMap<MediaResponseDto, MediaViewModel>();

            CreateMap<MediaResponseDto, MediaResponseViewModel>();

            CreateMap<CreateTextMediaElementViewModel, CreateTextMediaElementRequestDto>()
                .ForMember(d => d.Media, o => o.Ignore())
                .ForMember(d => d.Text, o => o.Ignore())
                .ForMember(d => d.Type, o => o.ResolveUsing(src =>
                {
                    return src.Media == null ? null : MediaTypeHelper.GetByContentType(src.Media.ContentType);
                }));

            CreateMap<TextMediaElementResponseDto, TextMediaResponseViewModel>();

            CreateMap<UpdateTextMediaElementViewModel, UpdateTextMediaElementRequestDto>()
                .ForMember(d => d.Media, m => m.Ignore())
                .ForMember(d => d.Text, m => m.Ignore())
                .ForMember(d => d.Type, o => o.ResolveUsing(src =>
                {
                    return src.Media == null ? null : MediaTypeHelper.GetByContentType(src.Media.ContentType);
                }));

            CreateMap<MeasurementResponseDto, MeasurementElementViewModel>();

            CreateMap<AssessmentResponseDto, AssessmentElementViewModel>();

            CreateMap<ProtocolResponseDto, ProtocolResponseViewModel>();

            CreateMap<ProtocolElementResponseDto, ProtocolElementResponseViewModel>();

            CreateMap<OpenEndedAnswerSetResponseDto, AnswerSetElementResponseViewModel>().ConvertUsing<AnswerSetConverter>();

            CreateMap<ScaleAnswerSetResponseDto, ScaleAnswerSetElementResponseViewModel>();

            CreateMap<SelectionAnswerSetResponseDto, SelectionAnswerSetElementResponseViewModel>();

            CreateMap<QuestionElementResponseDto, QuestionProtocolElementResponseViewModel>();

            CreateMap<MeasurementResponseDto, MeasurementProtocolElementResponseViewModel>();

            CreateMap<AssessmentResponseDto, AssessmentProtocolElementResponseViewModel>();

            CreateMap<TextMediaElementResponseDto, TextMediaProtocolElementResponseViewModel>();

            CreateMap<ElementDto, ElementResponseViewModel>()
                .ConvertUsing<ElementConverter>();

            CreateMap<ElementDto, QuestionProtocolElementResponseViewModel>();

            CreateMap<CreateSelectionAnswerSetViewModel, CreateSelectionAnswerSetRequestDto>()
                .ForMember(d => d.SelectionAnswerChoices, m => m.Ignore());

            CreateMap<UpdateSelectionAnswerSetViewModel, UpdateSelectionAnswerSetRequestDto>()
                .ForMember(d => d.SelectionAnswerChoices, m => m.Ignore());

            CreateMap<CreateSelectionAnswerChoiceViewModel, CreateSelectionAnswerChoiceRequestDto>()
                .ForMember(d => d.AnswerString, m => m.Ignore());

            CreateMap<UpdateSelectionAnswerChoiceViewModel, UpdateSelectionAnswerChoiceRequestDto>();

            CreateMap<UpdateSelectionAnswerChoiceRequestDto, SelectionAnswerChoiceViewModel>();

            CreateMap<SelectionAnswerChoiceResponseDto, SelectionAnswerChoiceViewModel>();

            CreateMap<BaseLocalizedStringViewModel, CreateLocalizedStringRequestDto>();

            CreateMap<BaseLocalizedStringViewModel, UpdateLocalizedStringRequestDto>();

            CreateMap<UpdateSelectionAnswerSetRequestDto, SelectionAnswerSetViewModel>();

            CreateMap<CreateSelectionAnswerChoiceRequestDto, CreateSelectionAnswerChoiceViewModel>();

            CreateMap<LocalizedStringResponseDto, BaseLocalizedStringViewModel>();

            CreateMap<SelectionAnswerSetResponseDto, SearchSelectionAnswerSetResultViewModel>();

            CreateMap<SelectionAnswerChoiceResponseDto, CreateSelectionAnswerChoiceViewModel>();

            CreateMap<SearchEntryDto, SearchEntryResponseViewModel>().ConvertUsing<SearchEntryConverter>();

            CreateMap<SearchTextAndMediaDto, SearchTextAndMediaResponseViewModel>()
                .ForMember(d => d.ThumbnailUrl, m => m.MapFrom(e => MediaTypeHelper.GenerateThumbnailUrl(e.MediaType, e.ThumbnailUrl)));
        }
    }
}