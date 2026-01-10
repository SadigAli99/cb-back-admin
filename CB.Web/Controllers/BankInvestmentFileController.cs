using CB.Application.DTOs.BankInvestmentFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.BankInvestmentFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankInvestmentFileController : ControllerBase
    {
        private readonly IBankInvestmentFileService _bankInvestmentFileService;
        private readonly BankInvestmentFileCreateValidator _createValidator;
        private readonly BankInvestmentFileEditValidator _editValidator;

        public BankInvestmentFileController(
            IBankInvestmentFileService bankInvestmentFileService,
            BankInvestmentFileCreateValidator createValidator,
            BankInvestmentFileEditValidator editValidator
        )
        {
            _bankInvestmentFileService = bankInvestmentFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BankInvestmentFileGetDTO> data = await _bankInvestmentFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            BankInvestmentFileGetDTO? data = await _bankInvestmentFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Bank xidmətlərinin investisiyası fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BankInvestmentFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _bankInvestmentFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("BankInvestment fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Bank xidmətlərinin investisiyası fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BankInvestmentFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _bankInvestmentFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Bank xidmətlərinin investisiyası fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("BankInvestment fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bankInvestmentFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Bank xidmətlərinin investisiyası fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Bank xidmətlərinin investisiyası fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
