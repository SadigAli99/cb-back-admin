
namespace CB.Application.DTOs.CustomerFeedback
{
    public class CustomerFeedbackEditDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
