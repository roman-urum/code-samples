using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Tag> tagsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TagService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.tagsRepository = this.unitOfWork.CreateRepository<Tag>();
        }

        /// <summary>
        /// Finds the tags.
        /// </summary>
        /// <param name="tagNames">The tag names.</param>
        /// <returns></returns>
        public async Task<IList<Tag>> FindTags(IList<string> tagNames = null)
        {
            Expression<Func<Tag, bool>> expression = PredicateBuilder.True<Tag>();

            if (tagNames != null && tagNames.Any())
            {
                expression = expression
                    .And(t => tagNames.Any(tn => tn.Equals(t.Name, StringComparison.InvariantCultureIgnoreCase)));
            }

            var tags = (await tagsRepository
                .FindAsync(
                    expression,
                    o => o.OrderBy(t => t.Name)))
                .Distinct()
                .ToList();

            return tags;
        }

        /// <summary>
        /// Builds the tags list.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<IList<Tag>> BuildTagsList(IList<string> tags, int customerId)
        {
            if (tags == null || !tags.Any())
            {
                return new List<Tag>();
            }

            tags = tags.Select(s => s.ToLower()).Distinct().ToList();
            var existingTags = (await FindTags(tags.ToList())).ToList();

            var newTags = tags
                .Distinct()
                .Where(t => existingTags.All(et => !et.Name.Equals(t, StringComparison.InvariantCultureIgnoreCase)))
                .Select(t => new Tag { Name = t, CustomerId = customerId });

            existingTags.AddRange(newTags);

            return existingTags;
        }

        /// <summary>
        /// Removes the unused tags.
        /// </summary>
        /// <returns>
        /// The list of removed Tags.
        /// </returns>
        public async Task<IList<string>> RemoveUnusedTags()
        {
            var unusedTags = (await tagsRepository
                .FindAsync(
                    t => !t.Conditions.Any(),
                    o => o.OrderBy(t => t.Name)))
                .ToList();

            var unusedTagNames = unusedTags.Select(t => t.Name).ToList();

            tagsRepository.DeleteRange(unusedTags);
            await unitOfWork.SaveAsync();

            return unusedTagNames;
        }
    }
}
