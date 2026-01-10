using CB.Application.DTOs.InstantPaymentFAQ;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InstantPaymentFAQ;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InstantPaymentFAQController : ControllerBase
    {
        private readonly IInstantPaymentFAQService _instantPaymentFAQService;
        private readonly InstantPaymentFAQCreateValidator _createValidator;
        private readonly InstantPaymentFAQEditValidator _editValidator;

        public InstantPaymentFAQController(
            IInstantPaymentFAQService instantPaymentFAQService,
            InstantPaymentFAQCreateValidator createValidator,
            InstantPaymentFAQEditValidator editValidator
        )
        {
            _instantPaymentFAQService = instantPaymentFAQService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InstantPaymentFAQGetDTO> data = await _instantPaymentFAQService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InstantPaymentFAQGetDTO? data = await _instantPaymentFAQService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tez-tez verilən suallar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstantPaymentFAQCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _instantPaymentFAQService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tez-tez verilən suallar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tez-tez verilən suallar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InstantPaymentFAQEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _instantPaymentFAQService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tez-tez verilən suallar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tez-tez verilən suallar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _instantPaymentFAQService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Tez-tez verilən suallar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tez-tez verilən suallar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
