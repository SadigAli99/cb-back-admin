
namespace CB.Application.DTOs.ControlMeasure
{
    public class ControlMeasureGetDTO
    {
        public int Id { get; set; }
        public string? ControlMeasureCategoryTitle { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
