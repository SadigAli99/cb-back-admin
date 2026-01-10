
namespace CB.Application.DTOs.ControlMeasure
{
    public class ControlMeasureEditDTO
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public int ControlMeasureCategoryId { get; set; }

    }
}
