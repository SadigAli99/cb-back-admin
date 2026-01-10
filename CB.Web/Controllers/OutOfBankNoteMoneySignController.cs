using CB.Application.DTOs.OutOfBankNoteMoneySign;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OutOfBankNoteMoneySign;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OutOfBankNoteMoneySignController : ControllerBase
    {
        private readonly IOutOfBankNoteMoneySignService _coinMoneySignService;
        private readonly OutOfBankNoteMoneySignCreateValidator _createValidator;
        private readonly OutOfBankNoteMoneySignEditValidator _editValidator;

        public OutOfBankNoteMoneySignController(
            IOutOfBankNoteMoneySignService coinMoneySignService,
            OutOfBankNoteMoneySignCreateValidator createValidator,
            OutOfBankNoteMoneySignEditValidator editValidator
        )
        {
            _coinMoneySignService = coinMoneySignService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OutOfBankNoteMoneySignGetDTO> data = await _coinMoneySignService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OutOfBankNoteMoneySignGetDTO? data = await _coinMoneySignService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OutOfBankNoteMoneySignCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _coinMoneySignService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədavüldən kənar metal pul nişanları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] OutOfBankNoteMoneySignEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _coinMoneySignService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tədavüldən kənar metal pul nişanları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _coinMoneySignService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədavüldən kənar metal pul nişanları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
