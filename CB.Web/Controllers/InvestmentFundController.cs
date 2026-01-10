using CB.Application.DTOs.InvestmentFund;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InvestmentFund;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvestmentFundController : ControllerBase
    {
        private readonly IInvestmentFundService _investmentFundService;
        private readonly InvestmentFundCreateValidator _createValidator;
        private readonly InvestmentFundEditValidator _editValidator;

        public InvestmentFundController(
            IInvestmentFundService investmentFundService,
            InvestmentFundCreateValidator createValidator,
            InvestmentFundEditValidator editValidator
        )
        {
            _investmentFundService = investmentFundService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InvestmentFundGetDTO> data = await _investmentFundService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InvestmentFundGetDTO? data = await _investmentFundService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İnvestisiya fondları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InvestmentFundCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _investmentFundService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İnvestisiya fondları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İnvestisiya fondları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InvestmentFundEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _investmentFundService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İnvestisiya fondları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İnvestisiya fondları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _investmentFundService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("İnvestisiya fondları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İnvestisiya fondları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
