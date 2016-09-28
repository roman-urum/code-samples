using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Media> mediaRepository;

        private readonly Dictionary<MediaType, List<string>> mediaTypesToMimeTypes = new Dictionary<MediaType, List<string>>()
        {
            {
                MediaType.Video,
                Settings
                    .SupportedExtensionsAndMimeTypes
                    .Where(kvp => kvp.Key == ".mp4")
                    .SelectMany(e => e.Value)
                    .Distinct()
                    .ToList()
            },
            {
                MediaType.Audio,
                Settings
                    .SupportedExtensionsAndMimeTypes
                    .Where(kvp => kvp.Key == ".mp3" || kvp.Key == ".m4a" || kvp.Key == ".wav" || kvp.Key == ".webm")
                    .SelectMany(e => e.Value)
                    .Where(e => !e.Contains("video/"))
                    .Distinct()
                    .ToList()
            },
            {
                MediaType.Document,
                Settings
                    .SupportedExtensionsAndMimeTypes
                    .Where(kvp => kvp.Key == ".pdf")
                    .SelectMany(e => e.Value)
                    .Distinct()
                    .ToList()
            },
            {
                MediaType.Image,
                Settings
                    .SupportedExtensionsAndMimeTypes
                    .Where(kvp => kvp.Key == ".jpg" || kvp.Key == ".jpeg" || kvp.Key == ".png")
                    .SelectMany(e => e.Value)
                    .Distinct()
                    .ToList()
            }
        };

        public MediaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mediaRepository = this.unitOfWork.CreateGenericRepository<Media>();
        }

        public async Task<Media> Create(Media media)
        {
            this.mediaRepository.Insert(media);
            await this.unitOfWork.SaveAsync();

            return media;
        }

        public async Task<Media> Get(int customerId, Guid mediaId)
        {
            var media = (await mediaRepository.FindAsync(m => !m.IsDeleted && m.CustomerId == customerId && m.Id == mediaId)).FirstOrDefault();

            return media;
        }

        public async Task<DeleteMediaStatus> Delete(int customerId, Guid mediaId)
        {
            var media = await Get(customerId, mediaId);

            if (media == null) return DeleteMediaStatus.NotFound;

            if (media.TextMediaElementsToMedias.Any() || media.LocalizedStrings.Any()) return DeleteMediaStatus.InUse;

            this.mediaRepository.Delete(media);
            await this.unitOfWork.SaveAsync();

            return DeleteMediaStatus.Success;
        }

        public async Task Update(Media media)
        {
            this.mediaRepository.Update(media);

            await this.unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Gets the medias.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<Media>> GetMedias(int customerId, MediaSearchDto request)
        {
            Expression<Func<Media, bool>> expression = m => !m.IsDeleted && m.CustomerId == customerId;

            if (request != null)
            {
                if (request.Tags != null && request.Tags.Any())
                {
                    Expression<Func<Media, bool>> tagsExpression = PredicateBuilder.False<Media>(); ;

                    foreach (var tag in request.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(m => m.Name.Contains(term));
                    }
                }

                if (request.Types != null && request.Types.Any())
                {
                    var contentTypes = new List<string>();

                    foreach (var mediaType in request.Types)
                    {
                        List<string> mimeTypes;

                        if (mediaTypesToMimeTypes.TryGetValue(mediaType, out mimeTypes))
                        {
                            contentTypes.AddRange(mimeTypes);
                        }
                    }

                    expression = expression.And(m => contentTypes.Any(t => t.ToLower() == m.ContentType.ToLower()));
                }
            }

            return await mediaRepository.FindPagedAsync(
                expression,
                m => m.OrderBy(e => e.ContentType).ThenBy(e => e.Name),
                new List<Expression<Func<Media, object>>>
                {
                    e => e.Tags
                },
                request != null ? request.Skip : (int?)null,
                request != null ? request.Take : (int?)null
            );
        }
    }
}