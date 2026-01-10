using CB.Application.DTOs.InformationIssuer;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InformationIssuer;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InformationIssuerController : ControllerBase
    {
        private readonly IInformationIssuerService _informationIssuerService;
        private readonly InformationIssuerCreateValidator _createValidator;
        private readonly InformationIssuerEditValidator _editValidator;

        public InformationIssuerController(
            IInformationIssuerService informationIssuerService,
            InformationIssuerCreateValidator createValidator,
            InformationIssuerEditValidator editValidator
        )
        {
            _informationIssuerService = informationIssuerService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InformationIssuerGetDTO> data = await _informationIssuerService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InformationIssuerGetDTO? data = await _informationIssuerService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Qiymətli kağız məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InformationIssuerCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _informationIssuerService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Qiymətli kağız məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Qiymətli kağız məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InformationIssuerEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _informationIssuerService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Qiymətli kağız məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Qiymətli kağız məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _informationIssuerService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Qiymətli kağız məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Qiymətli kağız məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
