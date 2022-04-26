using AutoMapper;
using CompatriotsClub.Data;
using Service.Base;
using Service.Catalogue;
using ViewModel.Catalogue;

namespace CompatriotsClub.Controllers
{

    public class FamilyController : BaseCRUDController<Family, FamilyViewModel, FamilyResponseViewModel, IFamilyService>
    {
        public FamilyController(IFamilyService service, IMapper mapper, IBaseService<Family> baseService) : base(service, mapper, baseService)
        {
        }
    }
}
