
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.EventImage
{
    public class EventImageCreateDTO
    {
        public int EventId { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new();
    }
}
