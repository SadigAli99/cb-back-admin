using CB.Application.DTOs.InterbankCardCenter;
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
    public class InterbankCardCenterController : ControllerBase
    {
        private readonly IInterbankCardCenterService _interbankCardCenterService;
        private readonly IValidator<InterbankCardCenterPostDTO> _validator;

        public InterbankCardCenterController(
            IInterbankCardCenterService interbankCardCenterService,
             IValidator<InterbankCardCenterPostDTO> validator
              )
        {
            _interbankCardCenterService = interbankCardCenterService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _interbankCardCenterService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InterbankCardCenterPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _interbankCardCenterService.CreateOrUpdate(dto);

            Log.Information("Banklararası kart məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
