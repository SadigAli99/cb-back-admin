

namespace CB.Application.DTOs.ControlMeasureCategory
{
    public class ControlMeasureCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
