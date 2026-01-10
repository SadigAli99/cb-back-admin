using CB.Application.DTOs.AnniversaryCoin;
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
    public class AnniversaryCoinController : ControllerBase
    {
        private readonly IAnniversaryCoinService _anniversaryCoinService;
        private readonly IValidator<AnniversaryCoinPostDTO> _validator;

        public AnniversaryCoinController(
            IAnniversaryCoinService anniversaryCoinService,
             IValidator<AnniversaryCoinPostDTO> validator
              )
        {
            _anniversaryCoinService = anniversaryCoinService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _anniversaryCoinService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] AnniversaryCoinPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _anniversaryCoinService.CreateOrUpdate(dto);

            Log.Information("Yubiley pul nişanı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
