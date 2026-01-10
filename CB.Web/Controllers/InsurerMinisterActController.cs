using CB.Application.DTOs.InsurerMinisterAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsurerMinisterAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurerMinisterActController : ControllerBase
    {
        private readonly IInsurerMinisterActService _insurerMinisterActService;
        private readonly InsurerMinisterActCreateValidator _createValidator;
        private readonly InsurerMinisterActEditValidator _editValidator;

        public InsurerMinisterActController(
            IInsurerMinisterActService insurerMinisterActService,
            InsurerMinisterActCreateValidator createValidator,
            InsurerMinisterActEditValidator editValidator
        )
        {
            _insurerMinisterActService = insurerMinisterActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsurerMinisterActGetDTO> data = await _insurerMinisterActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsurerMinisterActGetDTO? data = await _insurerMinisterActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığortaçı Nazirlər Kabinetinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsurerMinisterActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insurerMinisterActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığortaçı Nazirlər Kabinetinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığortaçı Nazirlər Kabinetinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsurerMinisterActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insurerMinisterActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığortaçı Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığortaçı Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insurerMinisterActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığortaçı Nazirlər Kabinetinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığortaçı Nazirlər Kabinetinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
