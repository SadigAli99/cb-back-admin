using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Poster
{
    public class PosterEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
    }
}
