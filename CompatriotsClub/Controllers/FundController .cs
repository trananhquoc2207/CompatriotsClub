using AutoMapper;
using CompatriotsClub.Data;
using Service.Base;
using Service.Catalogue;
using ViewModel.Catalogue;

namespace CompatriotsClub.Controllers
{

    public class FundController : BaseCRUDController<Fund, FundViewModel, FundResponseViewModel, IFundService>
    {
        public FundController(IFundService service, IMapper mapper, IBaseService<Fund> baseService) : base(service, mapper, baseService)
        {
        }
    }
}
