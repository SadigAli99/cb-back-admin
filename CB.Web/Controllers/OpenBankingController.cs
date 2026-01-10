using CB.Application.DTOs.OpenBanking;
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
    public class OpenBankingController : ControllerBase
    {
        private readonly IOpenBankingService _openBankingService;
        private readonly IValidator<OpenBankingPostDTO> _validator;

        public OpenBankingController(
            IOpenBankingService openBankingService,
             IValidator<OpenBankingPostDTO> validator
              )
        {
            _openBankingService = openBankingService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _openBankingService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] OpenBankingPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _openBankingService.CreateOrUpdate(dto);

            Log.Information("Açıq bankçılıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
