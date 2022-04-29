using AutoMapper;
using CompatriotsClub.Utilities;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Common;
using System.Net;

namespace CompatriotsClub.Controllers
{
#nullable disable
    [Route("v1/[controller]")]
    [ApiController]
    public class BaseCRUDController<T, K, V, S> : BaseApiController<S> where T : class where V : class where K : class
    {
        protected readonly IBaseService<T> _baseService;

        public BaseCRUDController(S service, IMapper mapper, IBaseService<T> baseService) : base(service, mapper)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// Get All Items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<ResponseWrapper>> Get()
        {
            try
            {
                List<T> results = await _baseService.GetAll();
                return ok_get(_mapper.Map<List<V>>(results));
            }
            catch (Exception e)
            {
                return error(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Get Item By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Get(int id)
        {
            try
            {

                var item = await _baseService.GetById(id);
                return ok_get(_mapper.Map<V>(item));
            }
            catch (Exception e)
            {
                return error(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Delete By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Remove(int id)
        {
            try
            {
                var isExisted = await _baseService.Exist(id);
                if (!isExisted) return notFound();
                await _baseService.Delete(id);
                return ok_delete();
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }
        /// <summary>
        /// Create Item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult<ResponseWrapper>> Post([FromBody] K request)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(error(GetModelStateErrMsg())); ;

                var item = await _baseService.Insert(_mapper.Map<T>(request));

                return ok_create(_mapper.Map<V>(item));
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }
        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Put(int id, [FromBody] K request)
        {

            try
            {
                var isExisted = await _baseService.Exist(id);
                if (!isExisted) return notFound();
                var item = _mapper.Map<T>(request);
                await _baseService.Update(item, id);
                return ok_update();
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }

        protected Guid? GetUserId()
        {
            var claimConstants = User?.FindFirst(ClaimConstants.USER_ID)?.Value;
            if (claimConstants == null)
                return null;
            var userId = Guid.Parse(User?.FindFirst(ClaimConstants.USER_ID)?.Value);
            return userId;
        }
    }
}
