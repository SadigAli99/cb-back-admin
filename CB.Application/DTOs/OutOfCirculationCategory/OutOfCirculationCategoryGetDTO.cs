
namespace CB.Application.DTOs.OutOfCirculationCategory
{
    public class OutOfCirculationCategoryGetDTO
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
