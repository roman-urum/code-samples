using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.ContentStorage.Azure.Models;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// Mappings for media instances.
    /// </summary>
    public class MediaMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<BaseMediaRequestDto, Media>()
                .ForMember(d => d.OriginalStorageKey, m => m.Ignore())
                .ForMember(d => d.Tags, m => m.Ignore())
                .ForMember(d => d.OriginalFileName, m => m.Ignore());

            Mapper.CreateMap<Media, MediaResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags != null ? s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()) : null))
                .ForMember(d => d.MediaUrl, m => m.ResolveUsing<MediaUrlResolver>())
                .ForMember(d => d.ThumbnailUrl, m => m.ResolveUsing<ThumbnailUrlResolver>());

            Mapper.CreateMap<BaseMediaRequestDto, ContentModel>()
                .ForMember(d => d.Name, m => m.MapFrom(s => s.OriginalFileName))
                .ForMember(d => d.Base64String, m => m.MapFrom(s => s.Content));
        }
    }
}