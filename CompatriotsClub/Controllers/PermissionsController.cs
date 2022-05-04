using Microsoft.AspNetCore.Mvc;
using Service.System;
using ViewModel.System;

namespace CompatriotsClub.Controllers
{

    [Route("v1/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _service;
        public PermissionsController(IPermissionService service)
        {
            _service = service;
        }
        [HttpGet("GetPaged")]
        public async Task<ActionResult> GetPagedResult(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                var result = await _service.GetPagedResult(pageIndex, pageSize);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest((e.Message)); ;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("{id}/AddUser")]
        public async Task<ActionResult> AddUser([FromBody] AddPermissionToUserModel model, [FromRoute] Guid id)
        {
            try
            {
                var result = await _service.AddUser(model, id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest((e.Message)); ;
            }

        }

        [HttpPut("{id}/RemoveUser")]
        public async Task<ActionResult> RemoveUser([FromBody] RemovePermissionOfUserModel model, [FromRoute] Guid id)
        {
            try
            {
                var result = await _service.RemoveUser(model, id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest((e.Message)); ;
            }

        }

    }
}
