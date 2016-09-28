using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Common.Helpers;
using HealthLibrary.DataAccess;
using HealthLibrary.Domain.Entities;
using HealthLibrary.DomainLogic.Services.Interfaces;

namespace HealthLibrary.DomainLogic.Services.Implementations
{
    /// <summary>
    /// TagsService.
    /// </summary>
    /// <seealso cref="HealthLibrary.DomainLogic.Services.Interfaces.ITagsService" />
    public class TagsService : ITagsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Tag> tagsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TagsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.tagsRepository = this.unitOfWork.CreateGenericRepository<Tag>();
        }

        /// <summary>
        /// Finds the tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="tagNames">The tag names.</param>
        /// <returns></returns>
        public async Task<IList<Tag>> FindTags(int customerId, IList<string> tagNames = null)
        {
            Expression<Func<Tag, bool>> expression = t => t.CustomerId == customerId;

            if (tagNames != null && tagNames.Any())
            {
                expression = expression.And(t => tagNames.Contains(t.Name));
            }

            return await tagsRepository.FindAsync(
                expression,
                o => o.OrderBy(t => t.Name)
            );
        }

        /// <summary>
        /// Builds the tags list.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        public async Task<IList<Tag>> BuildTagsList(int customerId, IList<string> tags)
        {
            if (tags == null || !tags.Any())
            {
                return new List<Tag>();
            }

            tags = tags
                .Where(t => !string.IsNullOrEmpty(t))
                .Select(t => t.ToLower())
                .Distinct()
                .ToList();

            var existingTags = await FindTags(customerId, tags.ToList());
            var existingTagNames = existingTags.Select(t => t.Name.ToLower()).ToList();

            var newTags = tags
                .Distinct()
                .Where(t => !existingTagNames.Contains(t))
                .Select(t => new Tag { Name = t, CustomerId = customerId });

            existingTags.AddRange(newTags);

            return existingTags;
        }

        /// <summary>
        /// Removes the unused tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>
        /// The list of removed Tags.
        /// </returns>
        public async Task<IList<string>> RemoveUnusedTags(int customerId)
        {
            var unusedTags = (await tagsRepository
                .FindAsync(
                    t => t.CustomerId == customerId &&
                        !t.AnswerSets.Any() &&
                        !t.Elements.Any() &&
                        !t.LocalizedMedias.Any() &&
                        !t.Programs.Any() &&
                        !t.Protocols.Any(),
                    o => o.OrderBy(t => t.Name)))
                .ToList();

            var unusedTagNames = unusedTags.Select(t => t.Name).ToList();

            tagsRepository.DeleteRange(unusedTags);
            await unitOfWork.SaveAsync();

            return unusedTagNames;
        }
    }
}