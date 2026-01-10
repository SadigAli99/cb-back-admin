using CB.Application.DTOs.ComplaintIndexCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ComplaintIndexCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintIndexCategoryController : ControllerBase
    {
        private readonly IComplaintIndexCategoryService _complaintIndexCategoryService;
        private readonly ComplaintIndexCategoryCreateValidator _createValidator;
        private readonly ComplaintIndexCategoryEditValidator _editValidator;

        public ComplaintIndexCategoryController(
            IComplaintIndexCategoryService complaintIndexCategoryService,
            ComplaintIndexCategoryCreateValidator createValidator,
            ComplaintIndexCategoryEditValidator editValidator
        )
        {
            _complaintIndexCategoryService = complaintIndexCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/complaintIndexCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _complaintIndexCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/complaintIndexCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _complaintIndexCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Şikayət indeksi kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/complaintIndexCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComplaintIndexCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _complaintIndexCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Şikayət indeksi kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Şikayət indeksi kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/complaintIndexCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ComplaintIndexCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _complaintIndexCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Şikayət indeksi kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Şikayət indeksi kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/complaintIndexCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _complaintIndexCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Şikayət indeksi kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Şikayət indeksi kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
