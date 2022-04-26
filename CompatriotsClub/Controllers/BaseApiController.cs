using AutoMapper;
using CompatriotsClub.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace CompatriotsClub.Controllers
{
    [EnableCors("PolicyAll")]
    [Route("v1/[controller]")]
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
        protected readonly T _service;
        protected readonly IMapper _mapper;

        public BaseApiController(T service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        protected ResponseWrapper error(HttpStatusCode code, string msg)
        {
            HttpContext.Response.StatusCode = (int)code;
            return new ResponseWrapper(code, msg);
        }
        protected ResponseWrapper error(string msg)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new ResponseWrapper(HttpStatusCode.BadRequest, msg);
        }
        protected ResponseWrapper ok_get(object data, Meta meta = null)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseWrapper(HttpStatusCode.OK, data, meta);
        }

        protected ResponseWrapper ok_create(object data)
        {
            Response.StatusCode = (int)HttpStatusCode.Created;
            return new ResponseWrapper(HttpStatusCode.Created, data);
        }
        protected ResponseWrapper ok_create()
        {
            Response.StatusCode = (int)HttpStatusCode.Created;
            return new ResponseWrapper(HttpStatusCode.Created, null);
        }
        protected ResponseWrapper ok_update()
        {
            return ok_get(null);
        }

        protected ResponseWrapper ok_delete()
        {
            Response.StatusCode = (int)HttpStatusCode.NoContent;
            return null;
        }

        protected ResponseWrapper notFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new ResponseWrapper(HttpStatusCode.NotFound, "Not found");
        }

        protected ResponseWrapper notFound(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new ResponseWrapper(HttpStatusCode.NotFound, msg);
        }

        protected ResponseWrapper badRequest(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new ResponseWrapper(HttpStatusCode.BadRequest, msg);
        }

        protected string GetModelStateErrMsg()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
            }
            return stringBuilder.ToString();
        }



        protected string GetRoleNameFromToken()
        {
            var roleName = User?.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(roleName))
                throw new UnauthorizedAccessException();
            return roleName;
        }
        protected string GetUsernameFromToken()
        {
            return User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
