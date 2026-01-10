
namespace CB.Core.Entities
{
    public class EventContent : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public List<EventContentTranslation> Translations { get; set; } = new();
        public List<EventContentImage> Images { get; set; } = new();
    }
}
