using CB.Application.DTOs.InternationalFinancialOrganization;
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
    public class InternationalFinancialOrganizationController : ControllerBase
    {
        private readonly IInternationalFinancialOrganizationService _internationalFinancialOrganizationService;
        private readonly IValidator<InternationalFinancialOrganizationPostDTO> _validator;

        public InternationalFinancialOrganizationController(
            IInternationalFinancialOrganizationService internationalFinancialOrganizationService,
             IValidator<InternationalFinancialOrganizationPostDTO> validator
              )
        {
            _internationalFinancialOrganizationService = internationalFinancialOrganizationService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _internationalFinancialOrganizationService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InternationalFinancialOrganizationPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _internationalFinancialOrganizationService.CreateOrUpdate(dto);

            Log.Information("Beynəlxalq maliyyə təşkilatları ilə əməkdaşlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
