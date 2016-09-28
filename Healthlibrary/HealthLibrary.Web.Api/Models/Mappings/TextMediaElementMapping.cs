using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets;
using HealthLibrary.Web.Api.Models.Elements.TextMediaElements;
using HealthLibrary.Web.Api.Models.Mappings.Resolvers;
using Microsoft.Practices.ServiceLocation;

namespace HealthLibrary.Web.Api.Models.Mappings
{
    /// <summary>
    /// TextMediaElementMapping.
    /// </summary>
    public class TextMediaElementMapping : IClassMapping
    {
        /// <summary>
        /// Creates the map.
        /// </summary>
        public void CreateMap()
        {
            Mapper.CreateMap<CreateLocalizedStringRequestDto, TextMediaElementString>()
                .ForMember(d => d.AudioFileMedia, o => o.Ignore());

            Mapper.CreateMap<CreateTextMediaElementRequestDto, TextMediaElement>()
                .ForMember(d => d.TextMediaElementsToMedias, o => o.Ignore())
                .ForMember(d => d.MediaType, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.TextLocalizedStrings, o => o.Ignore())
                .ForMember(d => d.TextMediaElementsToMedias, o => o.Ignore())
                .ForMember(d => d.Tags, o => o.Ignore());
            
            Mapper.CreateMap<TextMediaElement, TextMediaElementResponseDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags.OrderBy(t => t.Name)))
                .ForMember(d => d.Text, o => o.ResolveUsing<TextMediaElementStringResolver>())
                .ForMember(d => d.Media, o => o.ResolveUsing<TextMediaElementMediaResolver>())
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.CustomerId))
                .AfterMap((s, d) =>
                {
                    if (d.Media == null && d.Text == null)
                    {
                        var defaultText = s.TextLocalizedStrings
                            .SingleOrDefault(e => e.Language == CareElementRequestContext.Current.DefaultLanguage);

                        if (defaultText != null)
                        {
                            d.Text = Mapper.Map<TextMediaElementString, LocalizedStringWithAudioFileMediaResponseDto>(defaultText);
                        }

                        var defaultMedia = s.TextMediaElementsToMedias
                            .SingleOrDefault(e => e.Language == CareElementRequestContext.Current.DefaultLanguage);

                        if (defaultMedia != null)
                        {
                            d.Media = Mapper.Map<Media, MediaResponseDto>(defaultMedia.Media);
                        }
                    }

                    if (d.Media == null)
                    {
                        d.MediaType = null;
                    }
                });
            
            Mapper.CreateMap<TextMediaElementString, LocalizedStringWithAudioFileMediaResponseDto>();
            
            Mapper.CreateMap<CreateScaleAnswerSetRequestDto, ScaleAnswerSet>();

            Mapper.CreateMap<UpdateLocalizedStringRequestDto, TextMediaElementString>()
                .ForMember(d => d.AudioFileMedia, o => o.Ignore());

            Mapper.CreateMap<TextMediaElement, SearchEntryDto>()
                .ForMember(d => d.Tags, m => m.MapFrom(s => s.Tags == null ? new List<string>() : s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name)))
                .ForMember(d => d.Type, m => m.MapFrom(s => SearchCategoryType.TextMediaElement))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.TextLocalizedStrings != null &&
                    s.TextLocalizedStrings.Any() ? s.TextLocalizedStrings.First(ls => ls.Language == Settings.DefaultLanguage).Value : string.Empty));
            
            Mapper.CreateMap<TextMediaElement, SearchTextAndMediaDto>()
                .ConvertUsing(s =>
                {
                    var text = string.Empty;

                    if (s.TextLocalizedStrings != null && s.TextLocalizedStrings.Any())
                    {
                        var defaultLocalizedString = s.TextLocalizedStrings.FirstOrDefault(ls => ls.Language == Settings.DefaultLanguage);
                        if (defaultLocalizedString != null) text = defaultLocalizedString.Value;
                    }

                    string imageName = null;
                    string mediaUrl = null;
                    string thumbnailUrl = null;
                    string contentType = null;

                    if (s.MediaType != null && (s.MediaType.Value == MediaType.Image || s.MediaType.Value == MediaType.Video) &&
                        s.TextMediaElementsToMedias != null && s.TextMediaElementsToMedias
                        .Any(t => t.Media != null && !string.IsNullOrEmpty(t.Media.OriginalStorageKey)))
                    {
                        var media = s.TextMediaElementsToMedias.First().Media;
                        imageName = media.Name;

                        var contentStorage = ServiceLocator.Current.GetInstance<IContentStorage>();
                        mediaUrl = contentStorage.GenerateContentSASUrl(media.OriginalStorageKey, media.OriginalFileName);
                        thumbnailUrl = contentStorage.GenerateContentSASUrl(
                            media.ThumbnailStorageKey,
                            string.Concat(
                                Path.GetFileNameWithoutExtension(media.OriginalFileName),
                                "-thumbnail.jpg"
                            ));
                        contentType = media.ContentType;
                    }

                    return new SearchTextAndMediaDto()
                    {
                        Id = s.Id,
                        ImageName = imageName,
                        MediaUrl = mediaUrl,
                        ThumbnailUrl = thumbnailUrl,
                        ContentType = contentType,
                        MediaType = s.MediaType,
                        Name = s.Name,
                        Tags = s.Tags.OrderBy(t => t.Name, new NaturalSortComparer()).Select(t => t.Name).ToList(),
                        Text = text,
                        Type = SearchCategoryType.TextMediaElement
                    };
                });
        }
    }
}