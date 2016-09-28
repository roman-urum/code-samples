using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    public class TextMediaElementsService : ITextMediaElementsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TextMediaElement> textMediaElementsRepository;
        private readonly IProtocolService protocolService;

        public TextMediaElementsService(
            IUnitOfWork unitOfWork,
            IProtocolService protocolService
        )
        {
            this.unitOfWork = unitOfWork;
            this.textMediaElementsRepository = this.unitOfWork.CreateGenericRepository<TextMediaElement>();
            this.protocolService = protocolService;
        }

        public async Task<TextMediaElement> Create(TextMediaElement textMediaElement)
        {
            textMediaElement.Type = ElementType.TextMedia;

            textMediaElementsRepository.Insert(textMediaElement);

            await this.unitOfWork.SaveAsync();

            return textMediaElement;
        }

        public async Task<PagedResult<TextMediaElement>> GetElements(
            int customerId,
            TextMediaElementSearchDto searchRequest = null
        )
        {
            Expression<Func<TextMediaElement, bool>> expression = tm => !tm.IsDeleted && tm.CustomerId == customerId;

            if (searchRequest != null)
            {
                if (searchRequest.Tags != null && searchRequest.Tags.Any())
                {
                    Expression<Func<TextMediaElement, bool>> tagsExpression = PredicateBuilder.False<TextMediaElement>();

                    foreach (var tag in searchRequest.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(searchRequest.Q))
                {
                    var terms = searchRequest.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(tm => tm.Name.Contains(term));
                    }
                }

                if (searchRequest.Type.HasValue)
                {
                    expression = expression.And(tm => tm.MediaType == searchRequest.Type.Value);
                }
            }

            return await textMediaElementsRepository.FindPagedAsync(
                expression,
                o => o.OrderBy(e => e.Id),
                new List<Expression<Func<TextMediaElement, object>>>
                {
                    e => e.Tags,
                    e => e.TextLocalizedStrings,
                    e => e.TextMediaElementsToMedias.Select(t => t.Media)
                },
                searchRequest != null ? searchRequest.Skip : (int?)null,
                searchRequest != null ? searchRequest.Take : (int?)null
            );
        }

        public async Task<TextMediaElement> GetElement(
            int customerId,
            Guid textMediaElementId
        )
        {
            return (await textMediaElementsRepository
                .FindAsync(
                    el => el.CustomerId == customerId &&
                        el.Id == textMediaElementId &&
                        !el.IsDeleted,
                    o => o.OrderBy(prop => prop.Id)))
                .FirstOrDefault();
        }

        public async Task Update(TextMediaElement element)
        {
            this.textMediaElementsRepository.Update(element);

            await this.unitOfWork.SaveAsync();
        }

        public async Task<DeteleTextMediaElementStatus> Delete(
            int customerId,
            Guid textMediaElementId,
            string language = null
        )
        {
            if (!string.IsNullOrEmpty(language) &&
                language.ToLower() == Settings.DefaultLanguage.ToLower())
            {
                return DeteleTextMediaElementStatus.DeleteLocaleForbidden;
            }

            var element = await this.GetElement(customerId, textMediaElementId);

            if (element == null)
            {
                return DeteleTextMediaElementStatus.NotFound;
            }

            if (string.IsNullOrEmpty(language))
            {
                if (await this.protocolService.IsElementInUse(customerId, textMediaElementId))
                {
                    return DeteleTextMediaElementStatus.ElementIsInUse;
                }

                element.IsDeleted = true;
            }
            else
            {
                element.TextLocalizedStrings.RemoveRange(
                    element.TextLocalizedStrings.Where(e => e.Language == language).ToList()
                );

                element.TextMediaElementsToMedias.RemoveRange(
                    element.TextMediaElementsToMedias.Where(e => e.Language == language).ToList()
                );
            }

            await this.unitOfWork.SaveAsync();

            return DeteleTextMediaElementStatus.Success;
        }
    }
}