using CB.Application.DTOs.IssuerType;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.IssuerType;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssuerTypeController : ControllerBase
    {
        private readonly IIssuerTypeService _issuerTypeService;
        private readonly IssuerTypeCreateValidator _createValidator;
        private readonly IssuerTypeEditValidator _editValidator;

        public IssuerTypeController(
            IIssuerTypeService issuerTypeService,
            IssuerTypeCreateValidator createValidator,
            IssuerTypeEditValidator editValidator
        )
        {
            _issuerTypeService = issuerTypeService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/issuerType
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _issuerTypeService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/issuerType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _issuerTypeService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Emitent növü məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/issuerType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssuerTypeCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _issuerTypeService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Emitent növü əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/issuerType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] IssuerTypeEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _issuerTypeService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Emitent növü yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Emitent növü yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/issuerType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _issuerTypeService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Emitent növü silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Emitent növü uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
