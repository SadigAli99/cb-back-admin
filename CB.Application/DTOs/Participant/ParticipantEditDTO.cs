
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Participant
{
    public class ParticipantEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
        public int ParticipantCategoryId { get; set; }
    }
}
