
namespace CB.Core.Entities
{
    public class Event : BaseEntity
    {
        public DateTime Date { get; set; }
        public List<EventTranslation> Translations { get; set; } = new();
        public List<EventContent> Contents { get; set; } = new();
        public List<EventMedia> Medias { get; set; } = new();
    }
}
