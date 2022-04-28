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
        [HttpGet]
        public async Task<ActionResult> GetPagedResult(int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            var result = await _service.GetPagedResult(pageIndex, pageSize);
            return Ok(result);
        }

        [HttpPost("{id}/AddUser")]
        public async Task<ActionResult> AddUser([FromBody] AddPermissionToUserModel model, [FromRoute] Guid id)
        {
            var result = await _service.AddUser(model, id);
            return Ok(result);
        }

        [HttpPut("{id}/RemoveUser")]
        public async Task<ActionResult> RemoveUser([FromBody] RemovePermissionOfUserModel model, [FromRoute] Guid id)
        {
            var result = await _service.RemoveUser(model, id);
            return Ok(result);
        }
    }
}
