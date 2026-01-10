using CB.Application.DTOs.InformationType;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InformationType;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InformationTypeController : ControllerBase
    {
        private readonly IInformationTypeService _informationTypeService;
        private readonly InformationTypeCreateValidator _createValidator;
        private readonly InformationTypeEditValidator _editValidator;

        public InformationTypeController(
            IInformationTypeService informationTypeService,
            InformationTypeCreateValidator createValidator,
            InformationTypeEditValidator editValidator
        )
        {
            _informationTypeService = informationTypeService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/informationType
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _informationTypeService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/informationType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _informationTypeService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("MEAS Məlumat növü məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/informationType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InformationTypeCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _informationTypeService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("MEAS Məlumat növü əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/informationType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InformationTypeEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _informationTypeService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("MEAS Məlumat növü yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("MEAS Məlumat növü yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/informationType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _informationTypeService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("MEAS Məlumat növü silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("MEAS Məlumat növü uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
