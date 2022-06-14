using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
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
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private CompatriotsClubContext _dbContext;
        private DbSet<T> _dbSet;

        public BaseRepository(CompatriotsClubContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public CompatriotsClubContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        /// <summary>
        /// Get entity by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            if (allowTracking)
                return _dbSet.FirstOrDefault(predicate);

            return _dbSet.AsNoTracking().FirstOrDefault(predicate);
        }


        /// <summary>
        /// Get entity by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TResult Get<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        )
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query.Where(predicate);

            if (orderBy != null)
            {
                if (allowTracking)
                    return orderBy(query).Select(selector).FirstOrDefault();
                return orderBy(query).AsNoTracking().Select(selector).FirstOrDefault();
            }
            else
            {
                if (allowTracking)
                    return query.Select(selector).FirstOrDefault();
                return query.AsNoTracking().Select(selector).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(Guid id, bool allowTracking = true)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            if (allowTracking)
                return _dbSet.Where(predicate).AsEnumerable();

            return _dbSet.Where(predicate).AsNoTracking().AsEnumerable();
        }


        /// <summary>
        /// Get entites by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TResult> GetMany<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        )
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query.Where(predicate);

            if (orderBy != null)
            {
                if (allowTracking)
                    return orderBy(query).Select(selector).AsEnumerable();
                return orderBy(query).AsNoTracking().Select(selector).AsEnumerable();
            }
            else
            {
                if (allowTracking)
                    return query.Select(selector).AsEnumerable();
                return query.AsNoTracking().Select(selector).AsEnumerable();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(bool allowTracking = true)
        {
            if (allowTracking)
                return _dbSet.AsEnumerable();

            return _dbSet.AsNoTracking().AsEnumerable();
        }


        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TResult> GetAll<TResult>(
            Expression<Func<T, TResult>> selector,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        )
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                if (allowTracking)
                    return orderBy(query).Select(selector).AsEnumerable();
                return orderBy(query).AsNoTracking().Select(selector).AsEnumerable();
            }
            else
            {
                if (allowTracking)
                    return query.Select(selector).AsEnumerable();
                return query.AsNoTracking().Select(selector).AsEnumerable();
            }
        }

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity"></param>
        public  void  Add(T entity)
        {
          _dbSet.Add(entity);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(object id)
        {
            T existing = _dbSet.Find(id);
            if (existing != null)
                _dbSet.Remove(existing);
        }

        /// <summary>
        /// Delete by expression
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> entities = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T entity in entities)
                _dbSet.Remove(entity);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Get entities from sql string
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> FromSqlQuery(string sql, bool allowTracking = true)
        {
            if (allowTracking)
            {
                return _dbSet.FromSqlRaw(sql).AsEnumerable();
            }

            return _dbSet.FromSqlRaw(sql).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            if (allowTracking)
                return await _dbSet.FirstOrDefaultAsync(predicate);

            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Get entities by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        )
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query.Where(predicate);

            if (orderBy != null)
            {
                if (allowTracking)
                    return await orderBy(query).Select(selector).FirstOrDefaultAsync();
                return await orderBy(query).AsNoTracking().Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                if (allowTracking)
                    return await query.Select(selector).FirstOrDefaultAsync();
                return await query.AsNoTracking().Select(selector).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(Guid id, bool allowTracking = true)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        public async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            if (allowTracking)
                return await _dbSet.Where(predicate).ToListAsync();

            return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Get entities by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<TResult>> GetManyAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        )
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query.Where(predicate);

            if (orderBy != null)
            {
                if (allowTracking)
                    return await orderBy(query).Select(selector).ToListAsync();
                return await orderBy(query).AsNoTracking().Select(selector).ToListAsync();
            }
            else
            {
                if (allowTracking)
                    return await query.Select(selector).ToListAsync();
                return await query.AsNoTracking().Select(selector).ToListAsync();
            }
        }

        /// <summary>
        /// Get all entities async
        /// </summary>
        /// <param name="allowTracking"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync(bool allowTracking = true)
        {
            return await _dbSet.ToListAsync();
        }


        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns></returns>
        public async Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool allowTracking = true
        )
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                if (allowTracking)
                    return await orderBy(query).Select(selector).ToListAsync();
                return await orderBy(query).AsNoTracking().Select(selector).ToListAsync();
            }
            else
            {
                if (allowTracking)
                    return await query.Select(selector).ToListAsync();
                return await query.AsNoTracking().Select(selector).ToListAsync();
            }
        }

        /// <summary>
        /// Get entities from sql string async
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FromSqlQueryAsync(string sql, bool allowTracking = true)
        {
            IEnumerable<T> result;
            if (allowTracking)
                result = await _dbSet.FromSqlRaw(sql).ToListAsync();

            result = await _dbSet.FromSqlRaw(sql).AsNoTracking().ToListAsync();
            return result;
        }

        /// <summary>
        /// Add new entity async
        /// </summary>
        /// <param name="entity"></param>
        public void AddAsync(T entity)
        {
            _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Update an entity async
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbSet.Attach(entity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteAsync(object id)
        {
            T existing = _dbSet.Find(id);
            if (existing != null)
                _dbSet.Remove(existing);
        }

        /// <summary>
        /// Delete by expression
        /// </summary>
        /// <param name="where"></param>
        public void DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _dbSet.Where(predicate).AsEnumerable();
            foreach (T entity in entities)
                _dbSet.Remove(entity);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }

}
