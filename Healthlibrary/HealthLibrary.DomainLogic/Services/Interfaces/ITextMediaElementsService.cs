using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    public interface ITextMediaElementsService
    {
        Task<TextMediaElement> Create(TextMediaElement textMediaElement);

        Task<PagedResult<TextMediaElement>> GetElements(
            int customerId,
            TextMediaElementSearchDto searchRequest = null
        );

        Task<TextMediaElement> GetElement(
            int customerId,
            Guid textMediaElementId
        );

        Task Update(TextMediaElement element);

        Task<DeteleTextMediaElementStatus> Delete(
            int customerId,
            Guid textMediaElementId, 
            string language = null
        );
    }
}