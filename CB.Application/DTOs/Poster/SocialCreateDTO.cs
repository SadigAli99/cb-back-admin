using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Poster
{
    public class PosterCreateDTO
    {
        public IFormFile File { get; set; } = null!;
    }
}
