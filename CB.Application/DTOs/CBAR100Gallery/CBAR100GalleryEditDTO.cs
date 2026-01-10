
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CBAR100Gallery
{
    public class CBAR100GalleryEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
    }
}
