using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.System;
using ViewModel.System;

namespace CompatriotsClub.Controllers
{
    public class PermissionsController : BaseCRUDController<Permission, PermissionViewModel, PermissionResponseViewModel, IPermissionService>
    {
        public PermissionsController(IPermissionService service, IMapper mapper, IBaseService<Permission> baseService) : base(service, mapper, baseService)
        {
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
                return BadRequest(error(e.Message)); ;
            }
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
                return BadRequest(error(e.Message)); ;
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
                return BadRequest(error(e.Message)); ;
            }

        }
    }
}
