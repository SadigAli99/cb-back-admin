using CB.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RateController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;
        public RateController(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] DateTime date, IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            Log.Information("Fayl oxunmağa başladı : " + stream);
            await _excelImportService.ImportRatesAsync(stream, date);
            Log.Information("Valyuta məlumatları uğurla yeniləndi : {@Date}", date);
            return Ok(new { status = "success", message = "Valyuta məlumatları uğurla yeniləndi" });
        }

    }
}
