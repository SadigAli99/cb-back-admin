
namespace CB.Application.DTOs.CustomerFeedback
{
    public class CustomerFeedbackCreateDTO
    {
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
