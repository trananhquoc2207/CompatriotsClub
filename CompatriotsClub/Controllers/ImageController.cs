using AutoMapper;
using CompatriotsClub.Entities;
using CompatriotsClub.Utilities;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
using ViewModel.Catalogue;

namespace CompatriotsClub.Controllers
{
    public class ImageController : BaseApiController<IImageService>
    {
        private readonly IBaseService<Image> _baseService;
        public ImageController(IBaseService<Image> baseService, IImageService employeeService, IMapper mapper) : base(employeeService, mapper)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseWrapper>> Get([FromQuery] GetImagePagingRequest request)
        {
            try
            {
                var employees = await _service.GetAllPaging(request);
                return ok_get(employees);
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper>> Delete([FromRoute] int id)
        {
            try
            {
                var employees = await _service.Delete(id);
                if (employees)
                    return ok_get(employees);
                return BadRequest(error("Can not delete"));
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWrapper>> Put([FromRoute] int id, [FromForm] ImageUpdateViewModel request)
        {

            try
            {
                var isExisted = await _baseService.Exist(id);
                if (!isExisted) return notFound();
                var image = await _service.Update(id, request);
                // var item = _mapper.Map<Image>(request);
                //  await _baseService.Update(item, id);
                if (image)
                    return ok_update();
                return BadRequest(error("Can not update"));
            }
            catch (Exception e)
            {
                return BadRequest(error(e.Message));
            }
        }
    }
}
