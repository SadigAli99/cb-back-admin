using CB.Application.DTOs.FinancialInstitution;
using CB.Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialInstitutionController : ControllerBase
    {
        private readonly IFinancialInstitutionService _financialInstitutionService;
        private readonly IValidator<FinancialInstitutionPostDTO> _validator;

        public FinancialInstitutionController(
            IFinancialInstitutionService financialInstitutionService,
             IValidator<FinancialInstitutionPostDTO> validator
              )
        {
            _financialInstitutionService = financialInstitutionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialInstitutionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FinancialInstitutionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialInstitutionService.CreateOrUpdate(dto);

            Log.Information("Sistem əhəmiyyətli maliyyə institutlarının siyahısı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
