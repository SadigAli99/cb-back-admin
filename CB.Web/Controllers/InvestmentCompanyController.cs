using CB.Application.DTOs.InvestmentCompany;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InvestmentCompany;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvestmentCompanyController : ControllerBase
    {
        private readonly IInvestmentCompanyService _investmentCompanyService;
        private readonly InvestmentCompanyCreateValidator _createValidator;
        private readonly InvestmentCompanyEditValidator _editValidator;

        public InvestmentCompanyController(
            IInvestmentCompanyService investmentCompanyService,
            InvestmentCompanyCreateValidator createValidator,
            InvestmentCompanyEditValidator editValidator
        )
        {
            _investmentCompanyService = investmentCompanyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InvestmentCompanyGetDTO> data = await _investmentCompanyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InvestmentCompanyGetDTO? data = await _investmentCompanyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İnvestisiya şirkətləri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InvestmentCompanyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _investmentCompanyService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İnvestisiya şirkətləri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İnvestisiya şirkətləri məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InvestmentCompanyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _investmentCompanyService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İnvestisiya şirkətləri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İnvestisiya şirkətləri məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _investmentCompanyService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("İnvestisiya şirkətləri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İnvestisiya şirkətləri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
