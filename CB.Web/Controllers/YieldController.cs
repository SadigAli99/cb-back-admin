using CB.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YieldController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;
        public YieldController(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }

        [HttpPost("import-curves")]
        public async Task<IActionResult> ImportCurves([FromForm] DateTime date, IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");


            using var stream = file.OpenReadStream();
            Log.Information("Fayl oxunmağa başladı : " + stream);

            await _excelImportService.ImportYieldCurveAsync(stream, date);

            Log.Information("Gəlirlilik əyrisi bölməsinin yenilənməsi uğurludur : {@Date}", date);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpPost("import-durations")]
        public async Task<IActionResult> ImportDurations([FromForm] DateTime date, IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            Log.Information("Fayl oxunmağa başladı : " + stream);
            await _excelImportService.ImportYieldDurationAsync(stream, date);
            Log.Information("Gəlirlilik müddətləri bölməsinin yenilənməsi uğurludur : {@Date}", date);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpPost("import-parameters")]
        public async Task<IActionResult> ImportParameters([FromForm] DateTime date, IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            Log.Information("Fayl oxunmağa başladı : " + stream);
            await _excelImportService.ImportYieldParameterAsync(stream, date);
            Log.Information("Gəlirlilik parameterləri bölməsinin yenilənməsi uğurludur : {@Date}", date);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
