using AutoMapper;
using CompatriotsClub.Data;
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
    }
}
