
namespace CB.Core.Entities
{
    public class TrainingJournalist : BaseEntity
    {
        public List<TrainingJournalistTranslation> Translations { get; set; } = new();
        public List<TrainingJournalistImage> Images { get; set; } = new();
    }
}
