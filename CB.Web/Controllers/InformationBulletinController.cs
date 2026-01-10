using CB.Application.DTOs.InformationBulletin;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InformationBulletin;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InformationBulletinController : ControllerBase
    {
        private readonly IInformationBulletinService _infographicService;
        private readonly InformationBulletinCreateValidator _createValidator;
        private readonly InformationBulletinEditValidator _editValidator;

        public InformationBulletinController(
            IInformationBulletinService infographicService,
            InformationBulletinCreateValidator createValidator,
            InformationBulletinEditValidator editValidator
        )
        {
            _infographicService = infographicService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InformationBulletinGetDTO> data = await _infographicService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InformationBulletinGetDTO? data = await _infographicService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Maliyyə savadlılığı üzrə informasiya bülleten məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InformationBulletinCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _infographicService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Maliyyə savadlılığı üzrə informasiya bülleten məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Maliyyə savadlılığı üzrə informasiya bülleten məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InformationBulletinEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _infographicService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Maliyyə savadlılığı üzrə informasiya bülleten məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Maliyyə savadlılığı üzrə informasiya bülleten məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _infographicService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Maliyyə savadlılığı üzrə informasiya bülleten məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Maliyyə savadlılığı üzrə informasiya bülleten məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
