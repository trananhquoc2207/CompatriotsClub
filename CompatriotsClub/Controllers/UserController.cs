using AutoMapper;
using CompatriotsClub.Data;
using CompatriotsClub.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
using System.Net;
using ViewModel.System;

namespace CompatriotsClub.Controllers
{
    public class UserController : BaseCRUDController<AppUser, UserViewModel, UserResponseViewModel, IUserService>
    {
        public UserController(IUserService service, IMapper mapper, IBaseService<AppUser> baseService) : base(service, mapper, baseService)
        {
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ResponseWrapper>> Login(UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var login = await _service.Login(request);
                return new ResponseWrapper(HttpStatusCode.OK, login.Data);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ResponseWrapper>> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var login = await _service.Register(request);
                return ok_get(login);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedResult([FromQuery] UserFilter filter)
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

        [Authorize]
        [HttpGet("Permission")]
        public async Task<IActionResult> GetPermissionOfUser()
        {
            var userId = GetUserId();
            if (!userId.HasValue)
                return Unauthorized();
            try
            {
                var result = await _service.GetPermissionCodeOfUser(userId.Value);
                if (!result.Succeed)
                    return BadRequest(result.ErrorMessages);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message)); ;
            }

        }

        [HttpGet("{id}/Permission")]
        public async Task<IActionResult> GetPermissionOfUser([FromRoute] Guid id)
        {
            try
            {
                var result = await _service.GetPermissionDetailOfUser(id);
                if (!result.Succeed)
                    return BadRequest(result.ErrorMessages);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message)); ;
            }

        }
    }
}
