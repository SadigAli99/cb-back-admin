using CB.Application.DTOs.MethodologyExplain;
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
    public class MethodologyExplainController : ControllerBase
    {
        private readonly IMethodologyExplainService _methodologyExplainService;
        private readonly IValidator<MethodologyExplainPostDTO> _validator;

        public MethodologyExplainController(
            IMethodologyExplainService methodologyExplainService,
             IValidator<MethodologyExplainPostDTO> validator
              )
        {
            _methodologyExplainService = methodologyExplainService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _methodologyExplainService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MethodologyExplainPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _methodologyExplainService.CreateOrUpdate(dto);

            Log.Information("AZIR bölməsinin metodoloji izah məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
