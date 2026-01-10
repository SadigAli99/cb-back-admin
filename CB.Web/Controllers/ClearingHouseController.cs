using CB.Application.DTOs.ClearingHouse;
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
    public class ClearingHouseController : ControllerBase
    {
        private readonly IClearingHouseService _clearingHouseService;
        private readonly IValidator<ClearingHousePostDTO> _validator;

        public ClearingHouseController(
            IClearingHouseService clearingHouseService,
             IValidator<ClearingHousePostDTO> validator
              )
        {
            _clearingHouseService = clearingHouseService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _clearingHouseService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ClearingHousePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _clearingHouseService.CreateOrUpdate(dto);

            Log.Information("Klerinq təşkilatı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
