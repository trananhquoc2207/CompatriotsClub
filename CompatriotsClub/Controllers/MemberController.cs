using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
using ViewModel;

namespace CompatriotsClub.Controllers
{

    public class MemberController : BaseCRUDController<Member, MemberViewModel, MemberResponseViewModel, IMemberService>
    {
        public MemberController(IMemberService service, IMapper mapper, IBaseService<Member> baseService) : base(service, mapper, baseService)
        {
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedResult([FromQuery] MemberFilter filter)
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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                var result = await _service.Delete(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message)); ;
            }

        }
    }
}
