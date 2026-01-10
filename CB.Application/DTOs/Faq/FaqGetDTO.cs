
namespace CB.Application.DTOs.Faq
{
    public class FaqGetDTO
    {
        public int Id { get; set; }
        public string? FaqCategory { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
