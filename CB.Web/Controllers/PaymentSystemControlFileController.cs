using CB.Application.DTOs.PaymentSystemControlFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemControlFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemControlFileController : ControllerBase
    {
        private readonly IPaymentSystemControlFileService _PaymentSystemControlFileService;
        private readonly PaymentSystemControlFileCreateValidator _createValidator;
        private readonly PaymentSystemControlFileEditValidator _editValidator;

        public PaymentSystemControlFileController(
            IPaymentSystemControlFileService PaymentSystemControlFileService,
            PaymentSystemControlFileCreateValidator createValidator,
            PaymentSystemControlFileEditValidator editValidator
        )
        {
            _PaymentSystemControlFileService = PaymentSystemControlFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentSystemControlFileGetDTO> data = await _PaymentSystemControlFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemControlFileGetDTO? data = await _PaymentSystemControlFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəmə sistemlərinin nəzarət məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentSystemControlFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _PaymentSystemControlFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəmə sistemlərinin nəzarət məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəmə sistemlərinin nəzarət məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentSystemControlFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _PaymentSystemControlFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemlərinin nəzarət məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəmə sistemlərinin nəzarət məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _PaymentSystemControlFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəmə sistemlərinin nəzarət məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəmə sistemlərinin nəzarət məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
