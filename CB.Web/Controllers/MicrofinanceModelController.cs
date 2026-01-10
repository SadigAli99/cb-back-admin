using CB.Application.DTOs.MicrofinanceModel;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MicrofinanceModel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MicrofinanceModelController : ControllerBase
    {
        private readonly IMicrofinanceModelService _microfinanceModelService;
        private readonly MicrofinanceModelCreateValidator _createValidator;
        private readonly MicrofinanceModelEditValidator _editValidator;

        public MicrofinanceModelController(
            IMicrofinanceModelService microfinanceModelService,
            MicrofinanceModelCreateValidator createValidator,
            MicrofinanceModelEditValidator editValidator
        )
        {
            _microfinanceModelService = microfinanceModelService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MicrofinanceModelGetDTO> data = await _microfinanceModelService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MicrofinanceModelGetDTO? data = await _microfinanceModelService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Mikromaliyyə Modeli üzrə Strateji Çərçivə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MicrofinanceModelCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _microfinanceModelService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Metodologiya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Metodologiya məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MicrofinanceModelEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _microfinanceModelService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Metodologiya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Metodologiya məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _microfinanceModelService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Metodologiya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Metodologiya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
