using AutoMapper;
using CompatriotsClub.Data;
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
    }
}
