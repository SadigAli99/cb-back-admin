using CB.Application.DTOs.CoinMoneySign;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CoinMoneySign;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoinMoneySignController : ControllerBase
    {
        private readonly ICoinMoneySignService _coinMoneySignService;
        private readonly CoinMoneySignCreateValidator _createValidator;
        private readonly CoinMoneySignEditValidator _editValidator;

        public CoinMoneySignController(
            ICoinMoneySignService coinMoneySignService,
            CoinMoneySignCreateValidator createValidator,
            CoinMoneySignEditValidator editValidator
        )
        {
            _coinMoneySignService = coinMoneySignService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CoinMoneySignGetDTO> data = await _coinMoneySignService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CoinMoneySignGetDTO? data = await _coinMoneySignService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Metal pul nişanları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CoinMoneySignCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _coinMoneySignService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Metal pul nişanları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Metal pul nişanları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CoinMoneySignEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _coinMoneySignService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Metal pul nişanları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Metal pul nişanları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _coinMoneySignService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Metal pul nişanları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Metal pul nişanları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
