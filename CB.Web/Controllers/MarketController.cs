
using CB.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;
        public MarketController(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }

        [HttpPost("import-market-degree")]
        public async Task<IActionResult> ImportMarketDegrees([FromForm] IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = file.OpenReadStream();
            Log.Information("Fayl oxunmağa başladı : " + stream);

            await _excelImportService.ImportMarketDegreeAsync(stream);

            Log.Information("AZİR indeksi faiz dərəcəsi bölməsinin yenilənməsi uğurludur");
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpPost("import-market-information")]
        public async Task<IActionResult> ImportMarketInformation([FromForm] IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = file.OpenReadStream();
            Log.Information("Fayl oxunmağa başladı : " + stream);

            await _excelImportService.ImportMarketInformationAsync(stream);

            Log.Information("AZİR indeksi ilə bağlı məlumatlar bölməsinin yenilənməsi uğurludur");
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpPost("import-index-period")]
        public async Task<IActionResult> ImportIndexDegree([FromForm] IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = file.OpenReadStream();
            Log.Information("Fayl oxunmağa başladı : " + stream);

            await _excelImportService.ImportIndexPeriodAsync(stream);

            Log.Information("Dövr üzrə AZIR məlumatları bölməsinin yenilənməsi uğurludur");
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpPost("import-index-increasing")]
        public async Task<IActionResult> ImportIndexIncreasing([FromForm] IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Fayl seçilməyib");

            using var stream = file.OpenReadStream();
            Log.Information("Fayl oxunmağa başladı : " + stream);

            await _excelImportService.ImportIndexIncreasingAsync(stream);

            Log.Information("Artan AZIR məlumatları bölməsinin yenilənməsi uğurludur");
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

    }
}
