using CB.Application.DTOs.ElectronicMoneyInstitutionCaption;
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
    public class ElectronicMoneyInstitutionCaptionController : ControllerBase
    {
        private readonly IElectronicMoneyInstitutionCaptionService _electronicMoneyInstitutionCaptionService;
        private readonly IValidator<ElectronicMoneyInstitutionCaptionPostDTO> _validator;

        public ElectronicMoneyInstitutionCaptionController(
            IElectronicMoneyInstitutionCaptionService electronicMoneyInstitutionCaptionService,
             IValidator<ElectronicMoneyInstitutionCaptionPostDTO> validator
              )
        {
            _electronicMoneyInstitutionCaptionService = electronicMoneyInstitutionCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _electronicMoneyInstitutionCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ElectronicMoneyInstitutionCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _electronicMoneyInstitutionCaptionService.CreateOrUpdate(dto);

            Log.Information("Elektronik pul təşkilatı başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
