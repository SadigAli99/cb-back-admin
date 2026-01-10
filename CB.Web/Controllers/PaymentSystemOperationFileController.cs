using CB.Application.DTOs.PaymentSystemOperationFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemOperationFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemOperationFileController : ControllerBase
    {
        private readonly IPaymentSystemOperationFileService _paymentSystemOperationFileService;
        private readonly PaymentSystemOperationFileCreateValidator _createValidator;
        private readonly PaymentSystemOperationFileEditValidator _editValidator;

        public PaymentSystemOperationFileController(
            IPaymentSystemOperationFileService paymentSystemOperationFileService,
            PaymentSystemOperationFileCreateValidator createValidator,
            PaymentSystemOperationFileEditValidator editValidator
        )
        {
            _paymentSystemOperationFileService = paymentSystemOperationFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentSystemOperationFileGetDTO> data = await _paymentSystemOperationFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemOperationFileGetDTO? data = await _paymentSystemOperationFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəmə sistemlərinin operatorları fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentSystemOperationFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentSystemOperationFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("PaymentSystemOperation fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəmə sistemlərinin operatorları fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentSystemOperationFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentSystemOperationFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemlərinin operatorları fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("PaymentSystemOperation fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemOperationFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəmə sistemlərinin operatorları fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəmə sistemlərinin operatorları fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
