using Application.Catalog;
using AutoMapper;
using CompatriotsClub.Entities;
using CompatriotsClub.Utilities;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using ViewModels.Catalog.Posts;

namespace CompatriotsClub.Controllers
{
    public class CommentController : BaseApiController<ICommentService>
    {
        private readonly IBaseService<Comment> _baseService;
        public CommentController(ICommentService service, IMapper mapper, IBaseService<Comment> baseService) : base(service, mapper)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseWrapper>> Get([FromQuery] GetCommentPagingRequest request)
        {
            try
            {
                var employees = await _service.GetAllPaging(request);
                return ok_get(employees);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ResponseWrapper>> Post([FromBody] CommentAddViewModel request)
        {
            try
            {
                var employees = await _service.Add(request);
                return ok_get(employees);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper>> Delete([FromRoute] int id)
        {
            try
            {
                var employees = await _service.Delete(id);
                if (employees)
                    return ok_get(employees);
                return BadRequest(error("Can not delete"));
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }
    }
}
