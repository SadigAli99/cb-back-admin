using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Contact
{
    public class ContactPostDTO
    {
        public IFormFile? File { get; set; }
        public string? ContactMail { get; set; }
        public string? Map { get; set; }
        public Dictionary<string, string> Notes { get; set; } = new();
        public Dictionary<string, string> RegistrationTimes { get; set; } = new();
    }

}
