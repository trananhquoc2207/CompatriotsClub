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
            var result = await _service.GetPagedResult(filter);
            return Ok(result);
        }
    }
}
