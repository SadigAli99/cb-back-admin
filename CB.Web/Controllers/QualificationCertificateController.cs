using CB.Application.DTOs.QualificationCertificate;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.QualificationCertificate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QualificationCertificateController : ControllerBase
    {
        private readonly IQualificationCertificateService _qualificationCertificateService;
        private readonly QualificationCertificateCreateValidator _createValidator;
        private readonly QualificationCertificateEditValidator _editValidator;

        public QualificationCertificateController(
            IQualificationCertificateService qualificationCertificateService,
            QualificationCertificateCreateValidator createValidator,
            QualificationCertificateEditValidator editValidator
        )
        {
            _qualificationCertificateService = qualificationCertificateService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<QualificationCertificateGetDTO> data = await _qualificationCertificateService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            QualificationCertificateGetDTO? data = await _qualificationCertificateService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İxtisas şəhadətnamələri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] QualificationCertificateCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _qualificationCertificateService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İxtisas şəhadətnamələri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İxtisas şəhadətnamələri məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] QualificationCertificateEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _qualificationCertificateService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İxtisas şəhadətnamələri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İxtisas şəhadətnamələri məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _qualificationCertificateService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İxtisas şəhadətnamələri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İxtisas şəhadətnamələri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
