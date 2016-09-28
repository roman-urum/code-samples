using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Dtos;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.DataAccess
{
    /// <summary>
    /// Generic storage for persisted domain entities.
    /// </summary>
    public interface IRepository<T> where T : Entity
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(T entity);

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void InsertRange(ICollection<T> entities);

        /// <summary>
        /// Attaches the specified entity (in common used for stub updates).
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Attach(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="updateWholeEntity">if set to <c>true</c> updates whole
        /// entity otherwise updates only modified fields.</param>
        void Update(T entity, bool updateWholeEntity = false);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified range of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteRange(ICollection<T> entities);

        /// <summary>
        /// Provides a point to query entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        List<T> Find(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        );

        /// <summary>
        /// Provides a point to query entities (async).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        );

        /// <summary>
        /// Provides a point to query paged entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        PagedResult<T> FindPaged(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        );

        /// <summary>
        /// Provides a point to query paged entities (async).
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<PagedResult<T>> FindPagedAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? startIndex = null,
            int? limit = null
        );

        /// <summary>
        /// Returns count of entities matches to specified filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
    }
}
