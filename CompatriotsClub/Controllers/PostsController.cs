using AutoMapper;
using CompatriotsClub.Entities;
using CompatriotsClub.Utilities;
using Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
using Service.Common;
using System.Net;
using ViewModel.Catalogue;
using ViewModels.Catalog.Posts;

namespace CompatriotsClub.Controllers
{
    public class PostsController : BaseApiController<IPostService>
    {
        private readonly IBaseService<Post> _baseService;
        public PostsController(IPostService service, IMapper mapper, IBaseService<Post> baseService) : base(service, mapper)
        {
            _baseService = baseService;
        }
        //[HttpGet("Fill")]
        //public ActionResult<ResponseWrapper> Get([FromQuery] GetPostsRequest request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(error(GetModelStateErrMsg())); ;

        //        var item = _service.Fill(request, out Meta meta);

        //        return ok_get(item.ToList(), meta);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(error(e.Message));
        //    }
        //}

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedResult([FromQuery] GetPostsRequest filter)
        {
            try
            {
                var result = await _service.GetPagedResult(filter);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message)); ;
            }
        }

        [HttpPost("CreateImage/{postId}")]
        public async Task<ActionResult<ResponseWrapper>> SaveFileToAttendance([FromForm] ImageAddViewModel request, int postId)
        {
            try
            {
                await _service.AddImage(request, postId);
                return ok_create();
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }
        [HttpGet("GetImage/{postId}")]
        public async Task<ActionResult<ResponseWrapper>> GetImage(int postId)
        {
            try
            {
                var result = await _service.GetImage(postId);
                return ok_get(result);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Delete(int id)
        {
            try
            {
                var isExisted = await _baseService.Exist(id);
                if (!isExisted) return notFound();
                await _baseService.Delete(id);
                return new ResponseWrapper(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Get(int id)
        {
            try
            {
                var result = await _service.GetByPostId(id);
                return ok_get(result);
            }
            catch (Exception e)
            {
                return error(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<ResponseWrapper>> Post([FromBody] PostAddViewModel request)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(error(GetModelStateErrMsg()));
                var post = _mapper.Map<Post>(request);
                var item = await _baseService.Insert(post);
                var postViewModel = _mapper.Map<PostResponseViewModel>(item);
                return ok_create(postViewModel);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Put(int id, [FromBody] PostViewModel request)
        {

            try
            {
                var isExisted = await _baseService.Exist(id);
                if (!isExisted) return notFound();
                var item = _mapper.Map<Post>(request);
                await _baseService.Update(item, id);
                return ok_update();
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }

        [HttpPut("Feed/{postId}")]
        public virtual async Task<ActionResult<ResponseWrapper>> Feed(int postId, [FromBody] ActionPost action)
        {

            try
            {
                var claimConstants = User?.FindFirst(ClaimConstants.USER_ID)?.Value;
                if (claimConstants == null)
                    return Unauthorized();
                var userId = Guid.Parse(User?.FindFirst(ClaimConstants.USER_ID)?.Value);
                var actionSv = await _service.Feel(postId, action, userId);
                if (!actionSv.Succeed)
                {
                    return BadRequest(actionSv.ErrorMessages);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }
    }
}
