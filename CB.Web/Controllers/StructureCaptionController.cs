using CB.Application.DTOs.StructureCaption;
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
    public class StructureCaptionController : ControllerBase
    {
        private readonly IStructureCaptionService _structureCaptionService;
        private readonly IValidator<StructureCaptionPostDTO> _validator;

        public StructureCaptionController(
            IStructureCaptionService structureCaptionService,
             IValidator<StructureCaptionPostDTO> validator
              )
        {
            _structureCaptionService = structureCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _structureCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] StructureCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _structureCaptionService.CreateOrUpdate(dto);

            Log.Information("Struktur başlıq bölməsi məlumatının yenilənməsi uğurludur : {@Dto}", dto);

            return Ok(new { Status = "success", Message = "Məlumat uğurla yeniləndi" });
        }
    }
}
