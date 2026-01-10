
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CBAR100Gallery
{
    public class CBAR100GalleryCreateDTO
    {
        public IFormFile File { get; set; } = null!;
    }
}
