using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.Web.Api.Models.Elements.QuestionElements;

namespace HealthLibrary.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Resolver to map AnswerChoiceIdDto using question element data.
    /// </summary>
    public class AnswerChoiceIdResponseResolver : ValueResolver<QuestionElement, IEnumerable<AnswerChoiceIdDto>>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override IEnumerable<AnswerChoiceIdDto> ResolveCore(QuestionElement source)
        {
            return source.AnswerSet.Type == AnswerSetType.Scale
                ? source.QuestionElementToScaleAnswerChoices.Select(Mapper.Map<AnswerChoiceIdDto>)
                : source.QuestionElementToSelectionAnswerChoices.Select(Mapper.Map<AnswerChoiceIdDto>);
        }
    }
}