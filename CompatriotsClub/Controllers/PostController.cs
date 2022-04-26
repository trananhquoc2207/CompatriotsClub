using AutoMapper;
using CompatriotsClub.Data;
using Service.Base;
using Service.Catalogue;
using ViewModel.Catalogue;

namespace CompatriotsClub.Controllers
{

    public class PostController : BaseCRUDController<Post, PostViewModel, PostResponseViewModel, IPostService>
    {
        public PostController(IPostService service, IMapper mapper, IBaseService<Post> baseService) : base(service, mapper, baseService)
        {
        }
    }
}
