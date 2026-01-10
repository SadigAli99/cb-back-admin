using CB.Application.DTOs.BankInvestment;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.BankInvestment;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankInvestmentController : ControllerBase
    {
        private readonly IBankInvestmentService _bankInvestmentService;
        private readonly BankInvestmentCreateValidator _createValidator;
        private readonly BankInvestmentEditValidator _editValidator;

        public BankInvestmentController(
            IBankInvestmentService bankInvestmentService,
            BankInvestmentCreateValidator createValidator,
            BankInvestmentEditValidator editValidator
        )
        {
            _bankInvestmentService = bankInvestmentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BankInvestmentGetDTO> data = await _bankInvestmentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            BankInvestmentGetDTO? data = await _bankInvestmentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Bank xidmətlərinin investisiyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BankInvestmentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _bankInvestmentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Bank xidmətlərinin investisiyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Bank xidmətlərinin investisiyası məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BankInvestmentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _bankInvestmentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Bank xidmətlərinin investisiyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Bank xidmətlərinin investisiyası məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bankInvestmentService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Bank xidmətlərinin investisiyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Bank xidmətlərinin investisiyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
