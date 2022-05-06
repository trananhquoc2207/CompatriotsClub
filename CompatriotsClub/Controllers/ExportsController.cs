using Microsoft.AspNetCore.Mvc;
using Service.Export;
using ViewModel;

namespace CompatriotsClub.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ExportsController : ControllerBase
    {
#nullable disable
        private readonly IMemberExportService _memberExportService;
        public ExportsController(IMemberExportService memberExportService)
        {
            _memberExportService = memberExportService;
        }

        [HttpGet("ExportMember")]
        public async Task<IActionResult> ExportMember([FromQuery] MemberFilter filter)
        {
            try
            {
                var result = await _memberExportService.ExportMember(filter);
                var fileBytes = result.Data as byte[];
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", string.Format("DanhSachHoiNien.xlsx"));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
