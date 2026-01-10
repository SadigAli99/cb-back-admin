
namespace CB.Core.Entities
{
    public class ControlMeasureCategory : BaseEntity
    {
        public List<ControlMeasureCategoryTranslation> Translations { get; set; } = new();
    }
}
