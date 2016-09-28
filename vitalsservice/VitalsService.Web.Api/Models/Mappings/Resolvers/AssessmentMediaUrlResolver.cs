using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using VitalsService.ContentStorage.Azure.Services.Interfaces;
using VitalsService.Domain.DbEntities;

namespace VitalsService.Web.Api.Models.Mappings.Resolvers
{
    /// <summary>
    /// Resolver to map url of assessment media correct.
    /// </summary>
    public class AssessmentMediaUrlResolver : ValueResolver<AssessmentMedia, string>
    {
        /// <summary>
        /// Resolves the core.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override string ResolveCore(AssessmentMedia source)
        {
            if (source == null || string.IsNullOrEmpty(source.StorageKey))
            {
                return string.Empty;
            }

            var contentStorage = ServiceLocator.Current.GetInstance<IContentStorage>();

            return contentStorage.GenerateContentSASUrl(source.StorageKey, source.OriginalFileName);
        }
    }
}