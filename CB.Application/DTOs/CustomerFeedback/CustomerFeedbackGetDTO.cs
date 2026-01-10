
namespace CB.Application.DTOs.CustomerFeedback
{
    public class CustomerFeedbackGetDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
