
namespace CB.Core.Entities
{
    public class ParticipantCategory : BaseEntity
    {
        public List<ParticipantCategoryTranslation> Translations { get; set; } = new();
    }
}
