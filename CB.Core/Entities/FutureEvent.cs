
namespace CB.Core.Entities
{
    public class FutureEvent : BaseEntity
    {
        public DateTime Date { get; set; }
        public List<FutureEventTranslation> Translations { get; set; } = new();
    }
}
