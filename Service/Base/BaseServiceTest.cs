using AutoMapper;
using Data.Infrastructure;
using OpenQA.Selenium;
using Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public interface IBaseServiceTest<E, F, V, A, U> where E : class where F : class where V : class where A : class where U : class
    {
        Task<object> Add(A model);

        Task<object> Update(U model, Guid id);

        Task<object> Delete(Guid id);

        Task<V> Get(Guid id);

        Task<PagingModel<V>> GetPagedResult(F filter, Guid userId);
    }

    /*
        Generic
        - E: Entity
        - F: Filter model
        - V: View model
        - A: Add model
        - U: Update model
    */
    public class BaseService<E, F, V, A, U> : IBaseServiceTest<E, F, V, A, U> where E : class where F : class where V : class where A : class where U : class
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<E> _repository;

        public BaseService(IMapper mapper, IUnitOfWork unitOfWork, IBaseRepository<E> repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual async Task<object> Add(A model)
        {
            /* Builder */
            var entity = _mapper.Map<A, E>(model);
            /* Save */
            _repository.Add(entity);
            await _repository.DbContext.SaveChangesAsync();

            return null;
        }

        public virtual async Task<object> Update(U model, Guid id)
        {
            /* Validate */
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException();
            /* Builder */
            entity = _mapper.Map<U, E>(model, entity);
            /* Save */
            _repository.Update(entity);
            await _repository.DbContext.SaveChangesAsync();

            return id;
        }

        public virtual async Task<object> Delete(Guid id)
        {
            /* Validate */
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException();
            /* Save */
            _repository.Delete(id);
            await _repository.DbContext.SaveChangesAsync();

            return id;
        }

        public virtual async Task<V> Get(Guid id)
        {
            /* Validate */
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException();

            return _mapper.Map<E, V>(entity);
        }

        public virtual async Task<PagingModel<V>> GetPagedResult(F filter, Guid userId)
        {
            var result = new PagingModel<V>();
            /* Query */
            var entities = await _repository.GetAllAsync();
            /* Builder */
            var entityModels = _mapper.Map<List<E>, List<V>>(entities);

            result.TotalCounts = entities.Count;
            result.Data = entityModels;
            return result;
        }
    }

}
