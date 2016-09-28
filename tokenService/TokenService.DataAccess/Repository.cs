using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess
{
    /// <summary>
    /// The generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private DbContext context;
        private DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        #region IRepository<T> Members

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes the specified range of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void DeleteRange(ICollection<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(ICollection<T> entities)
        {
            dbSet.AddRange(entities);
        }

        /// <summary>
        /// Attaches the specified entity (in common used for stub updates).
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Attach(T entity)
        {
            dbSet.Attach(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="updateWholeEntity">if set to <c>true</c> updates whole
        /// entity otherwise updates only modified fields.</param>
        public virtual void Update(T entity, bool updateWholeEntity = false)
        {
            if (updateWholeEntity)
            {
                context.Entry(entity).State = EntityState.Modified;
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
        public virtual List<T> Find(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        )
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
        public virtual Task<List<T>> FindAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        )
        {
            var query = Query(filter, orderBy, includeProperties);

            query = ApplyPaging(query, startIndex, limit);

            return query.ToListAsync();
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
        public virtual PagedResult<T> FindPaged(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        )
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
        public virtual async Task<PagedResult<T>> FindPagedAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        )
        {
            var query = Query(filter, orderBy, includeProperties);

            var total = await query.LongCountAsync();

            query = ApplyPaging(query, startIndex, limit);

            return new PagedResult<T>()
            {
                Results = await query.ToListAsync(),
                Total = total
            };
        }

        /// <summary>
        /// Returns count of entities matches to specified filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            var query = Query(filter);

            return await query.CountAsync();
        }

        protected virtual IQueryable<T> Query(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null
        )
        {
            IQueryable<T> query = dbSet;

            if (includeProperties != null)
            {
                includeProperties.ForEach(i => { query = query.Include(i); });
            }

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
    }
}
