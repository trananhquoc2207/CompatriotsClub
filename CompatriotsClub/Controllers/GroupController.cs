using AutoMapper;
using CompatriotsClub.Data;
using Service.Base;
using Service.Catalogue;
using ViewModel.Catalogue;

namespace CompatriotsClub.Controllers
{

    public class GroupController : BaseCRUDController<Group, GroupViewModel, GroupResponseViewModel, IGroupService>
    {
        public GroupController(IGroupService service, IMapper mapper, IBaseService<Group> baseService) : base(service, mapper, baseService)
        {
        }
    }
}
