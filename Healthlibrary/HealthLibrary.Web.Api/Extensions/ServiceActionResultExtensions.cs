using AutoMapper;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.Web.Api.Extensions
{
    /// <summary>
    /// Extension methods for service action result.
    /// </summary>
    public static class ServiceActionResultExtensions
    {
        public static ServiceActionResult<TStatus, TClone> CloneWithMapping<TStatus, TContent, TClone>(
            this ServiceActionResult<TStatus, TContent> result, TClone newContent)
        {
            var mappingResult = Mapper.Map(result.Content, newContent);

            return new ServiceActionResult<TStatus, TClone>(result.Status, mappingResult);
        }
    }
}