using CB.Application.DTOs.FinancialDevelopment;
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
    public class FinancialDevelopmentController : ControllerBase
    {
        private readonly IFinancialDevelopmentService _financialDevelopmentService;
        private readonly IValidator<FinancialDevelopmentPostDTO> _validator;

        public FinancialDevelopmentController(
            IFinancialDevelopmentService financialDevelopmentService,
             IValidator<FinancialDevelopmentPostDTO> validator
              )
        {
            _financialDevelopmentService = financialDevelopmentService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialDevelopmentService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] FinancialDevelopmentPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialDevelopmentService.CreateOrUpdate(dto);

            Log.Information("Maliyyə inkişaf strategiyası məlumatları uğurla yeniləndi : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }
    }
}
