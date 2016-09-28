using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerService.DataAccess;
using CustomerService.DataAccess.Extensions;
using CustomerService.Domain;
using CustomerService.Domain.Dtos;

namespace CustomerService.DomainLogic.Tests
{
    public abstract class TestRepository<T, TId> : IRepository<T> 
        where T : Entity<TId>, new() 
        where TId : struct
    {
        /// <summary>
        /// List of test entities which will be used in repository.
        /// </summary>
        protected ICollection<T> TestDataSource { get; set; }

        public TestRepository()
        {
            TestDataSource = new Collection<T>();
        }

        #region IRepository members

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(T entity)
        {
            entity.Id = this.GetNextId();

            TestDataSource.Add(entity);
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void InsertRange(ICollection<T> entities)
        {
            TestDataSource.AddRange(entities);
        }

        /// <summary>
        /// Attaches the specified entity (in common used for stub updates).
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Attach(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="updateWholeEntity">if set to <c>true</c> updates whole
        /// entity otherwise updates only modified fields.</param>
        public void Update(T entity, bool updateWholeEntity = false)
        {
            var existingEntity = TestDataSource.FirstOrDefault(e => e.Id.Equals(entity.Id));

            TestDataSource.Remove(existingEntity);
            TestDataSource.Add(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            TestDataSource.Remove(entity);
        }

        /// <summary>
        /// Deletes the specified range of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void DeleteRange(ICollection<T> entities)
        {
            var listToRemove = entities.ToList();

            foreach (var entity in listToRemove)
            {
                TestDataSource.Remove(entity);
            }
        }

        /// <summary>
        /// Provides a point to query entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public List<T> Find(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null, int? startIndex = null,
            int? limit = null)
        {
            var query = Query(filter, orderBy, includeProperties);

            query = ApplyPaging(query, startIndex, limit);

            return query.ToList();
        }

        /// <summary>
        /// Provides a point to query entities (async).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public Task<List<T>> FindAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null, int? startIndex = null,
            int? limit = null)
        {
            var query = Query(filter, orderBy, includeProperties);

            query = ApplyPaging(query, startIndex, limit);

            return Task.FromResult(query.ToList());
        }

        /// <summary>
        /// Provides a point to query paged entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public PagedResult<T> FindPaged(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null, int? limit = null)
        {
            var query = Query(filter, orderBy, includeProperties);

            var total = query.LongCount();

            query = ApplyPaging(query, startIndex, limit);

            return new PagedResult<T>()
            {
                Results = query.ToList(),
                Total = total
            };
        }

        /// <summary>
        /// Provides a point to query paged entities (async).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public Task<PagedResult<T>> FindPagedAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null, int? startIndex = null,
            int? limit = null)
        {
            var query = Query(filter, orderBy, includeProperties);
            var total = query.LongCount();

            query = ApplyPaging(query, startIndex, limit);

            var result = new PagedResult<T>()
            {
                Results = query.ToList(),
                Total = total
            };

            return Task.FromResult(result);
        }

        /// <summary>
        /// Anies the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null)
        {
            return Task.FromResult(Query(filter).Any());
        }

        #endregion

        #region Help methods

        /// <summary>
        /// Clears all existing records in list.
        /// </summary>
        public void Refresh()
        {
            TestDataSource.Clear();
        }

        /// <summary>
        /// Replaces existing data with provided data source.
        /// </summary>
        /// <param name="testDataSource"></param>
        public void Refresh(ICollection<T> testDataSource)
        {
            TestDataSource = testDataSource;
        }

        #endregion

        #region Private methods

        private IQueryable<T> Query(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null
        )
        {
            IQueryable<T> query = TestDataSource.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        private IQueryable<T> ApplyPaging(
            IQueryable<T> query,
            int? startIndex = null,
            int? limit = null
        )
        {
            if (startIndex != null)
            {
                query = query.Skip(startIndex.Value);
            }

            if (limit != null)
            {
                query = query.Take(limit.Value);
            }

            return query;
        }

        #endregion

        /// <summary>
        /// Generates Id for new entity.
        /// </summary>
        /// <returns></returns>
        protected abstract TId GetNextId();
    }
}
