using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Z.EntityFramework.Plus;

namespace Service.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected CompatriotsClubContext _dbContext;
        protected DbSet<T> _dbSet;
        public BaseService(CompatriotsClubContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Delete(object id)
        {
            T entity = await GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await Save();
                ExpireCache();
            }
            else throw new Exception("Not found");
        }

        public async Task<bool> Exist(object id)
        {
            T entity = await GetById(id);
            return entity != null;
        }

        public async Task<List<T>> GetAll()
        {
            var cache = await GetFromCache();
            return cache.ToList();
        }

        public async Task<IEnumerable<T>> GetFromCache()
        {
            var query = _dbSet.AsQueryable().AsNoTracking();
            return await query.FromCacheAsync(typeof(T).FullName);
        }

        public async Task<T> GetById(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<T> Insert(T entity)
        {
            var item = await _dbSet.AddAsync(entity);
            await Save();
            ExpireCache();
            return item.Entity;
        }

        public async Task<List<T>> InsertRange(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await Save();
            ExpireCache();
            return entities;
        }

        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                handleDbUpdateEx(dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task Update(T entity, object id)
        {
            var originEntity = await GetById(id);
            _dbContext.Update(originEntity);
            await Save();
            ExpireCache();
        }


        public IQueryable<T> GetIQueryable()
        {
            return _dbSet;
        }
        private void handleDbUpdateEx(DbUpdateException dbEx)
        {
            if (dbEx.InnerException != null)
            {
                // dbEx.InnerException.InnerException is SqlException
                if (dbEx.InnerException.InnerException != null)
                    throw new Exception(dbEx.InnerException.InnerException.Message,
                                        dbEx.InnerException.InnerException);
                else throw new Exception(dbEx.InnerException.Message, dbEx.InnerException);
            }
            else throw new Exception(GetDbUpdateErrMsgs(dbEx), dbEx);
        }
        private string GetDbUpdateErrMsgs(DbUpdateException dbEx)
        {
            return dbEx.Message;
        }

        public async Task DeleteRange(List<object> ids)
        {
            List<T> deletedEntities = new List<T>();
            foreach (int id in ids)
            {
                var entity = await GetById(id);
                if (entity != null) deletedEntities.Add(entity);
            }
            _dbSet.RemoveRange(deletedEntities);
            await Save();
            ExpireCache();
        }

        public async Task<T> GetByIds(object id, object id2)
        {
            return await _dbSet.FindAsync(id, id2);
        }

        public async Task Delete(object id, object id2)
        {
            T entity = await GetByIds(id, id2);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await Save();
                ExpireCache();
            }
            else throw new Exception("Not found");
        }

        public async Task<bool> Exist(object id, object id2)
        {
            T entity = await GetByIds(id, id2);
            return entity != null;
        }


        public async Task<int> Update<D>(T entity)
        {
            PropertyInfo[] sourceProprties = typeof(D)
               .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //_dbContext.Entry(entity).State = EntityState.Unchanged;
            _dbSet.Attach(entity);
            foreach (var property in sourceProprties)
            {
                if (property.Name != "Id")
                    _dbContext.Entry(entity).Property(property.Name).IsModified = true;
            }
            return await _dbContext.SaveChangesAsync();
        }
        public void ExpireCache()
        {
            QueryCacheManager.ExpireTag(typeof(T).FullName);
        }
    }
}
