using CB.Application.DTOs.Statute;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Statute;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatuteController : ControllerBase
    {
        private readonly IStatuteService _statuteService;
        private readonly StatuteCreateValidator _createValidator;
        private readonly StatuteEditValidator _editValidator;

        public StatuteController(
            IStatuteService statuteService,
            StatuteCreateValidator createValidator,
            StatuteEditValidator editValidator
        )
        {
            _statuteService = statuteService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StatuteGetDTO> data = await _statuteService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            StatuteGetDTO? data = await _statuteService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Əsasnamə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StatuteCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _statuteService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Əsasnamə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Əsasnamə məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StatuteEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _statuteService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Əsasnamə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Əsasnamə məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statuteService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Əsasnamə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Əsasnamə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
