using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.QuestionElements;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// Mappings for question element instances.
    /// </summary>
    public class QuestionElementMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            // Create new quesion
            Mapper.CreateMap<CreateQuestionElementRequestDto, QuestionElement>()
                .BeforeMap((s, d) => d.LocalizedStrings = new Collection<QuestionElementString>())
                .BeforeMap(
                    (s, d) => d.QuestionElementToSelectionAnswerChoices =
                        new Collection<QuestionElementToSelectionAnswerChoice>())
                .BeforeMap(
                    (s, d) =>
                        d.QuestionElementToScaleAnswerChoices = new Collection<QuestionElementToScaleAnswerChoice>())
                .ForMember(d => d.LocalizedStrings, m => m.Ignore())
                .ForMember(d => d.QuestionElementToSelectionAnswerChoices, m => m.Ignore())
                .ForMember(d => d.QuestionElementToScaleAnswerChoices, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore());

            Mapper.CreateMap<CreateLocalizedStringRequestDto, QuestionElementString>();

            // Update question request
            Mapper.CreateMap<UpdateQuestionElementRequestDto, QuestionElement>()
                .BeforeMap((s, d) => d.LocalizedStrings = new Collection<QuestionElementString>())
                .BeforeMap(
                    (s, d) => d.QuestionElementToSelectionAnswerChoices =
                        new Collection<QuestionElementToSelectionAnswerChoice>())
                .BeforeMap(
                    (s, d) =>
                        d.QuestionElementToScaleAnswerChoices = new Collection<QuestionElementToScaleAnswerChoice>())
                .ForMember(d => d.LocalizedStrings, m => m.Ignore())
                .ForMember(d => d.QuestionElementToSelectionAnswerChoices, m => m.Ignore())
                .ForMember(d => d.QuestionElementToScaleAnswerChoices, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore());

            Mapper.CreateMap<UpdateLocalizedStringRequestDto, QuestionElementString>();

            // Get question requests
            Mapper.CreateMap<QuestionElement, QuestionElementResponseDto>()
                .ForMember(d => d.QuestionElementString, m => m.ResolveUsing<QuestionElementStringResponseResolver>())
                .ForMember(d => d.AnswerChoiceIds, m => m.ResolveUsing<AnswerChoiceIdResponseResolver>())
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name, new NaturalSortComparer())));

            Mapper.CreateMap<QuestionElementString, LocalizedStringWithAudioFileMediaResponseDto>();

            // Answer choice ids
            Mapper.CreateMap<AnswerChoiceIdDto, QuestionElementToScaleAnswerChoice>()
                .ForMember(d => d.Id, m => m.Ignore());

            Mapper.CreateMap<AnswerChoiceIdDto, QuestionElementToSelectionAnswerChoice>()
                .ForMember(d => d.SelectionAnswerChoiceId, m => m.MapFrom(s => s.Id));

            Mapper.CreateMap<QuestionElementToSelectionAnswerChoice, AnswerChoiceIdDto>()
                .ForMember(d => d.Value, m => m.Ignore())
                .ForMember(d => d.Id, m => m.MapFrom(s => s.SelectionAnswerChoiceId));

            Mapper.CreateMap<QuestionElementToScaleAnswerChoice, AnswerChoiceIdDto>()
                .ForMember(d => d.Id, m => m.Ignore());

            Mapper.CreateMap<QuestionElement, SearchEntryDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.QuestionElement))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.LocalizedStrings.First(ls => ls.Language == Settings.DefaultLanguage).Value));
        }
    }
}