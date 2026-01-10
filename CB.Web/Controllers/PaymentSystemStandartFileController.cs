using CB.Application.DTOs.PaymentSystemStandartFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemStandartFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentSystemStandartFileController : ControllerBase
    {
        private readonly IPaymentSystemStandartFileService _paymentSystemStandartFileService;
        private readonly PaymentSystemStandartFileCreateValidator _createValidator;
        private readonly PaymentSystemStandartFileEditValidator _editValidator;

        public PaymentSystemStandartFileController(
            IPaymentSystemStandartFileService paymentSystemStandartFileService,
            PaymentSystemStandartFileCreateValidator createValidator,
            PaymentSystemStandartFileEditValidator editValidator
        )
        {
            _paymentSystemStandartFileService = paymentSystemStandartFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/paymentSystemStandartFile
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _paymentSystemStandartFileService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/paymentSystemStandartFile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _paymentSystemStandartFileService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Ödəmə sistemi fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/paymentSystemStandartFile
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentSystemStandartFileCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _paymentSystemStandartFileService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Ödəmə sistemi fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Ödəmə sistemi fayl məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/paymentSystemStandartFile/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentSystemStandartFileEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _paymentSystemStandartFileService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemi fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Ödəmə sistemi fayl məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/paymentSystemStandartFile/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemStandartFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəmə sistemi fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Ödəmə sistemi fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
