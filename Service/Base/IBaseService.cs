namespace Service.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(object id);
        Task Delete(object id);
        Task<T> Insert(T entity);
        Task Update(T entity, object id);
        Task<bool> Exist(object id);
        Task<bool> Exist(object id, object id2);
        Task Save();
        Task<List<T>> InsertRange(List<T> entities);
        Task DeleteRange(List<object> ids);
        IQueryable<T> GetIQueryable();
        Task<IEnumerable<T>> GetFromCache();
    }
}
