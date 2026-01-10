using CB.Application.DTOs.ParticipantCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ParticipantCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParticipantCategoryController : ControllerBase
    {
        private readonly IParticipantCategoryService _participantCategoryService;
        private readonly ParticipantCategoryCreateValidator _createValidator;
        private readonly ParticipantCategoryEditValidator _editValidator;

        public ParticipantCategoryController(
            IParticipantCategoryService participantCategoryService,
            ParticipantCategoryCreateValidator createValidator,
            ParticipantCategoryEditValidator editValidator
        )
        {
            _participantCategoryService = participantCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/participantCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _participantCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/participantCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _participantCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("İştirakçı kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/participantCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParticipantCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _participantCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("İştirakçı kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("İştirakçı kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/participantCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ParticipantCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _participantCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("İştirakçı kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("İştirakçı kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/participantCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _participantCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İştirakçı kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("İştirakçı kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
