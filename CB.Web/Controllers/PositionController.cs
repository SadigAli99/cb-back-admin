using CB.Application.DTOs.Position;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Position;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly PositionCreateValidator _createValidator;
        private readonly PositionEditValidator _editValidator;

        public PositionController(
            IPositionService positionService,
            PositionCreateValidator createValidator,
            PositionEditValidator editValidator
        )
        {
            _positionService = positionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/position
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _positionService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/position/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _positionService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Vəzifə məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/position
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PositionCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _positionService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Vəzifə əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/position/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PositionEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _positionService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Vəzifə yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Vəzifə yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/position/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _positionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vəzifə silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Vəzifə uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
