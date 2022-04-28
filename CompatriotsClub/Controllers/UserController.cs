using AutoMapper;
using CompatriotsClub.Data;
using CompatriotsClub.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
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
                return ok_get(login);
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
    }
}
