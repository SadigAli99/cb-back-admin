using CB.Application.DTOs.GreenTaxonomy;
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
    public class GreenTaxonomyController : ControllerBase
    {
        private readonly IGreenTaxonomyService _greenTaxonomyService;
        private readonly IValidator<GreenTaxonomyPostDTO> _validator;

        public GreenTaxonomyController(
            IGreenTaxonomyService greenTaxonomyService,
             IValidator<GreenTaxonomyPostDTO> validator
              )
        {
            _greenTaxonomyService = greenTaxonomyService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _greenTaxonomyService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] GreenTaxonomyPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _greenTaxonomyService.CreateOrUpdate(dto);

            Log.Information("Yaşıl taksonomiya məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
