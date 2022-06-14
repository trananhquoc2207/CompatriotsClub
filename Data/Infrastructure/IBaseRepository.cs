using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
#nullable disable
    public interface IBaseRepository<T> where T : class
    {
        CompatriotsClubContext DbContext { get; }
        #region Sync Methods
        /// <summary>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate = null, bool allowTracking = true);

        /// <summary>
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        TResult Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        );

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        T GetById(Guid id, bool allowTracking = true);

        /// <summary>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate = null, bool allowTracking = true);

        /// <summary>
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        IEnumerable<TResult> GetMany<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        );

        /// <summary>
        /// </summary>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(bool allowTracking = true);

        /// <summary>
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        IEnumerable<TResult> GetAll<TResult>(
            Expression<Func<T, TResult>> selector,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        );

        /// <summary>
        /// Get entities from sql string
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        IEnumerable<T> FromSqlQuery(string sql, bool allowTracking = true);

        /// <summary>
        /// Add new antity
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(object id);

        /// <summary>
        /// Delete by expression
        /// </summary>
        /// <param name="where"></param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Delete the entities
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(IEnumerable<T> entities);

        #endregion

        #region Async Methods
        /// <summary>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, bool allowTracking = true);

        /// <summary>
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        );

        /// <summary>
        /// Get entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id, bool allowTracking = true);

        /// <summary>
        /// Get entities lambda expression async
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true);

        /// <summary>
        /// Get entities lambda expression async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TResult>> GetManyAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        );

        /// <summary>
        /// </summary>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync(bool allowTracking = true);

        /// <summary>
        /// Get all entities async
        /// </summary>
        /// <returns></returns>
        Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        );

        /// <summary>
        /// Add new entity async
        /// </summary>
        /// <param name="entity"></param>
        void AddAsync(T entity);

        /// <summary>
        /// Update an entity async
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAsync(T entity);

        /// <summary>
        /// Delete an entity async
        /// </summary>
        /// <param name="entity"></param>
        void DeleteAsync(object id);

        /// <summary>
        /// Delete by expression async
        /// </summary>
        /// <param name="where"></param>
        void DeleteAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// Delete the entities async
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Get entities from sql string async
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FromSqlQueryAsync(string sql, bool allowTracking = true);
        #endregion
    }

}
