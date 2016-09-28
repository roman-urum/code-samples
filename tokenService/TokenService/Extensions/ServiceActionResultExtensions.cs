using AutoMapper;
using CareInnovations.HealthHarmony.Maestro.TokenService.DomainLogic.Services.Results;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Extensions
{
    /// <summary>
    /// Extension methods for service action result.
    /// </summary>
    public static class ServiceActionResultExtensions
    {
        /// <summary>
        /// Creates copy of service action result and maps content to new specified type.
        /// </summary>
        /// <typeparam name="TStatus"></typeparam>
        /// <typeparam name="TContent"></typeparam>
        /// <typeparam name="TClone"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ServiceActionResult<TStatus, TClone> CloneWithMapping<TStatus, TContent, TClone>(
            this ServiceActionResult<TStatus, TContent> result, TClone newContent)
        {
            var mappingResult = Mapper.Map(result.Content, newContent);

            return new ServiceActionResult<TStatus, TClone>(result.Status, mappingResult);
        }
    }
}