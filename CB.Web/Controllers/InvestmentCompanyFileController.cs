using CB.Application.DTOs.InvestmentCompanyFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InvestmentCompanyFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvestmentCompanyFileController : ControllerBase
    {
        private readonly IInvestmentCompanyFileService _investmentCompanyFileService;
        private readonly InvestmentCompanyFileCreateValidator _createValidator;
        private readonly InvestmentCompanyFileEditValidator _editValidator;

        public InvestmentCompanyFileController(
            IInvestmentCompanyFileService investmentCompanyFileService,
            InvestmentCompanyFileCreateValidator createValidator,
            InvestmentCompanyFileEditValidator editValidator
        )
        {
            _investmentCompanyFileService = investmentCompanyFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InvestmentCompanyFileGetDTO> data = await _investmentCompanyFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InvestmentCompanyFileGetDTO? data = await _investmentCompanyFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İnvestisiya şirkətləri fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InvestmentCompanyFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _investmentCompanyFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("InvestmentCompany fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İnvestisiya şirkətləri fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InvestmentCompanyFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _investmentCompanyFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İnvestisiya şirkətləri fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("InvestmentCompany fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _investmentCompanyFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İnvestisiya şirkətləri fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İnvestisiya şirkətləri fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
