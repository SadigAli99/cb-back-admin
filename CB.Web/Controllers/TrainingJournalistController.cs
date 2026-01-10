using CB.Application.DTOs.TrainingJournalist;
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
    public class TrainingJournalistController : ControllerBase
    {
        private readonly ITrainingJournalistService _trainingJournalistService;
        private readonly IValidator<TrainingJournalistPostDTO> _validator;

        public TrainingJournalistController(
            ITrainingJournalistService trainingJournalistService,
             IValidator<TrainingJournalistPostDTO> validator
              )
        {
            _trainingJournalistService = trainingJournalistService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _trainingJournalistService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] TrainingJournalistPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _trainingJournalistService.CreateOrUpdate(dto);

            Log.Information("Təlim proqramı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        [HttpDelete("{eventId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int eventId, int imageId)
        {
            var result = await _trainingJournalistService.DeleteImageAsync(eventId, imageId);
            if (!result)
            {
                Log.Warning("Təlim proqramı şəkil silinə bilmədi : EventId = {@EventId}, ImageId = {@ImageId}", eventId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }
            Log.Information("Təlim proqramı şəkil uğurla silindi : EventId = {@EventId}, ImageId = {@ImageId}", eventId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }
    }
}
