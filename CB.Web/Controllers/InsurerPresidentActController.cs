using CB.Application.DTOs.InsurerPresidentAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsurerPresidentAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurerPresidentActController : ControllerBase
    {
        private readonly IInsurerPresidentActService _insurerPresidentActService;
        private readonly InsurerPresidentActCreateValidator _createValidator;
        private readonly InsurerPresidentActEditValidator _editValidator;

        public InsurerPresidentActController(
            IInsurerPresidentActService insurerPresidentActService,
            InsurerPresidentActCreateValidator createValidator,
            InsurerPresidentActEditValidator editValidator
        )
        {
            _insurerPresidentActService = insurerPresidentActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsurerPresidentActGetDTO> data = await _insurerPresidentActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsurerPresidentActGetDTO? data = await _insurerPresidentActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığortaçı AR prezidentinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsurerPresidentActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insurerPresidentActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığortaçı AR prezidentinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığortaçı AR prezidentinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsurerPresidentActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insurerPresidentActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığortaçı AR prezidentinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığortaçı AR prezidentinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insurerPresidentActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığortaçı AR prezidentinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığortaçı AR prezidentinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
