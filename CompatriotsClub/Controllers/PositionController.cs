using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Base;
using Service.Catalogue;
using ViewModel;

namespace CompatriotsClub.Controllers
{
    public class PositionController : BaseCRUDController<Position, PositionViewModel, PositionResponseViewModel, IPositionService>
    {
        public PositionController(IPositionService service, IMapper mapper, IBaseService<Position> baseService) : base(service, mapper, baseService)
        {
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedResult([FromQuery] PositionFilter filter)
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
    }
}
