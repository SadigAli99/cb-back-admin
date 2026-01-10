using CB.Application.DTOs.ElectronicMoneyInstitution;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ElectronicMoneyInstitution;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ElectronicMoneyInstitutionController : ControllerBase
    {
        private readonly IElectronicMoneyInstitutionService _electronicMoneyInstitutionService;
        private readonly ElectronicMoneyInstitutionCreateValidator _createValidator;
        private readonly ElectronicMoneyInstitutionEditValidator _editValidator;

        public ElectronicMoneyInstitutionController(
            IElectronicMoneyInstitutionService electronicMoneyInstitutionService,
            ElectronicMoneyInstitutionCreateValidator createValidator,
            ElectronicMoneyInstitutionEditValidator editValidator
        )
        {
            _electronicMoneyInstitutionService = electronicMoneyInstitutionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ElectronicMoneyInstitutionGetDTO> data = await _electronicMoneyInstitutionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ElectronicMoneyInstitutionGetDTO? data = await _electronicMoneyInstitutionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Elektronik pul təşkilatı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ElectronicMoneyInstitutionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _electronicMoneyInstitutionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Elektronik pul təşkilatı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Elektronik pul təşkilatı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ElectronicMoneyInstitutionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _electronicMoneyInstitutionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Elektronik pul təşkilatı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Elektronik pul təşkilatı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _electronicMoneyInstitutionService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Elektronik pul təşkilatı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Elektronik pul təşkilatı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
