using CB.Application.DTOs.BankNote;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.BankNote;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankNoteController : ControllerBase
    {
        private readonly IBankNoteService _bankNoteService;
        private readonly BankNoteCreateValidator _createValidator;
        private readonly BankNoteEditValidator _editValidator;

        public BankNoteController(
            IBankNoteService bankNoteService,
            BankNoteCreateValidator createValidator,
            BankNoteEditValidator editValidator
            )
        {
            _bankNoteService = bankNoteService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /BankNote
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _bankNoteService.GetAllAsync();
            return Ok(data);
        }

        // POST: /BankNote
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BankNoteCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool created = await _bankNoteService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Bank note məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Bank note məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // GET: /BankNote/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _bankNoteService.GetForEditAsync(id);
            if (data is null)
            {
                Log.Warning("Bank note məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // PUT : /BankNote/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, BankNoteEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            var updated = await _bankNoteService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Bank note məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Bank note məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /BankNote/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bankNoteService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Bank note məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Bank note məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
