using CB.Application.DTOs.CBAR105Event;
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
    public class CBAR105EventController : ControllerBase
    {
        private readonly ICBAR105EventService _CBAR105EventService;
        private readonly IValidator<CBAR105EventPostDTO> _validator;

        public CBAR105EventController(
            ICBAR105EventService CBAR105EventService,
             IValidator<CBAR105EventPostDTO> validator
              )
        {
            _CBAR105EventService = CBAR105EventService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _CBAR105EventService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] CBAR105EventPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _CBAR105EventService.CreateOrUpdate(dto);

            Log.Information("CBAR 105-ci ildönümü tədbir məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpDelete("{eventId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int eventId, int imageId)
        {
            var result = await _CBAR105EventService.DeleteImageAsync(eventId, imageId);
            if (!result)
            {
                Log.Warning("CBAR 105-ci il dönümü şəkil silinə bilmədi : EventId = {@EventId}, ImageId = {@ImageId}", eventId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }
            Log.Information("CBAR 105-ci il dönümü şəkil uğurla silindi : EventId = {@EventId}, ImageId = {@ImageId}", eventId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }
    }
}
