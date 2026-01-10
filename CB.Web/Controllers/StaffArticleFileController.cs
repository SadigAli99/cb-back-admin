using CB.Application.DTOs.StaffArticleFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StaffArticleFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffArticleFileController : ControllerBase
    {
        private readonly IStaffArticleFileService _staffArticleFileService;
        private readonly StaffArticleFileCreateValidator _createValidator;
        private readonly StaffArticleFileEditValidator _editValidator;

        public StaffArticleFileController(
            IStaffArticleFileService staffArticleFileService,
            StaffArticleFileCreateValidator createValidator,
            StaffArticleFileEditValidator editValidator
        )
        {
            _staffArticleFileService = staffArticleFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/staffArticleFile
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _staffArticleFileService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/staffArticleFile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _staffArticleFileService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/staffArticleFile
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StaffArticleFileCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _staffArticleFileService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("İşçi məqaləsi fayl məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/staffArticleFile/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StaffArticleFileEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _staffArticleFileService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("İşçi məqaləsi fayl məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/staffArticleFile/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _staffArticleFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("İşçi məqaləsi fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
