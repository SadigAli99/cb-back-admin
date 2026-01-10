using CB.Application.DTOs.VacancyDetail;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.VacancyDetail;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VacancyDetailController : ControllerBase
    {
        private readonly IVacancyDetailService _vacancyDetailService;
        private readonly VacancyDetailCreateValidator _createValidator;
        private readonly VacancyDetailEditValidator _editValidator;

        public VacancyDetailController(
            IVacancyDetailService vacancyDetailService,
            VacancyDetailCreateValidator createValidator,
            VacancyDetailEditValidator editValidator
        )
        {
            _vacancyDetailService = vacancyDetailService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<VacancyDetailGetDTO> data = await _vacancyDetailService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            VacancyDetailGetDTO? data = await _vacancyDetailService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Vakansiya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VacancyDetailCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _vacancyDetailService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Vakansiya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Vakansiya məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] VacancyDetailEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _vacancyDetailService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Vakansiya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Vakansiya məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _vacancyDetailService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vakansiya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Vakansiya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
