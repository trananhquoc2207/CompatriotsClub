using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
using ViewModel.Catalogue;

namespace CompatriotsClub.Controllers
{

    public class ContactController : BaseCRUDController<Contacts, ContactViewModel, ContactResponseViewModel, IContactService>
    {
        public ContactController(IContactService service, IMapper mapper, IBaseService<Contacts> baseService) : base(service, mapper, baseService)
        {
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedResult([FromQuery] ContactFilter filter)
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

        [HttpPost("{id}/AddMember")]
        public async Task<IActionResult> AddMember([FromRoute] int id, [FromBody] ContactMembersRequest request)
        {
            try
            {
                var result = await _service.AddMember(id, request);
                if (!result.Succeed)
                    return BadRequest(result.ErrorMessages);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message)); ;
            }
        }
        [HttpDelete("{id}/RemoveMember")]
        public async Task<IActionResult> RemoveMember([FromRoute] int id, [FromBody] int MemberId)
        {
            try
            {
                var result = await _service.RemoveMember(id, MemberId);
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
