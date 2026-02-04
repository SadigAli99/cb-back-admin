
namespace CB.Core.Entities
{
    public class ControlMeasure : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int ControlMeasureCategoryId { get; set; }
        public ControlMeasureCategory ControlMeasureCategory { get; set; } = null!;
        public List<ControlMeasureTranslation> Translations { get; set; } = new();
    }
}
