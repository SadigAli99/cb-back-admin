using CB.Application.DTOs.InternshipDirection;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InternshipDirection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InternshipDirectionController : ControllerBase
    {
        private readonly IInternshipDirectionService _internshipDirectionService;
        private readonly InternshipDirectionCreateValidator _createValidator;
        private readonly InternshipDirectionEditValidator _editValidator;

        public InternshipDirectionController(
            IInternshipDirectionService internshipDirectionService,
            InternshipDirectionCreateValidator createValidator,
            InternshipDirectionEditValidator editValidator
        )
        {
            _internshipDirectionService = internshipDirectionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InternshipDirectionGetDTO> data = await _internshipDirectionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InternshipDirectionGetDTO? data = await _internshipDirectionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Təcrübə proqramı istiqaməti məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InternshipDirectionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _internshipDirectionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Təcrübə proqramı istiqaməti məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Təcrübə proqramı istiqaməti məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InternshipDirectionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _internshipDirectionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Təcrübə proqramı istiqaməti məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Təcrübə proqramı istiqaməti məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _internshipDirectionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Təcrübə proqramı istiqaməti məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Təcrübə proqramı istiqaməti məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
