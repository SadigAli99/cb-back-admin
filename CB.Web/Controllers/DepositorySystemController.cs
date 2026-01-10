using CB.Application.DTOs.DepositorySystem;
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
    public class DepositorySystemController : ControllerBase
    {
        private readonly IDepositorySystemService _depositorySystemService;
        private readonly IValidator<DepositorySystemPostDTO> _validator;

        public DepositorySystemController(
            IDepositorySystemService depositorySystemService,
             IValidator<DepositorySystemPostDTO> validator
              )
        {
            _depositorySystemService = depositorySystemService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _depositorySystemService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] DepositorySystemPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _depositorySystemService.CreateOrUpdate(dto);

            Log.Information("Depositar mərkəz məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
