using CB.Application.DTOs.ComplaintIndex;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ComplaintIndex;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintIndexController : ControllerBase
    {
        private readonly IComplaintIndexService _complaintIndexService;
        private readonly ComplaintIndexCreateValidator _createValidator;
        private readonly ComplaintIndexEditValidator _editValidator;

        public ComplaintIndexController(
            IComplaintIndexService complaintIndexService,
            ComplaintIndexCreateValidator createValidator,
            ComplaintIndexEditValidator editValidator
        )
        {
            _complaintIndexService = complaintIndexService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/complaintIndex
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _complaintIndexService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/complaintIndex/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _complaintIndexService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Şikayət indeksi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/complaintIndex
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComplaintIndexCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _complaintIndexService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Şikayət indeksi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Şikayət indeksi məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/complaintIndex/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ComplaintIndexEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _complaintIndexService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Şikayət indeksi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Şikayət indeksi məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/complaintIndex/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _complaintIndexService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Şikayət indeksi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Şikayət indeksi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
