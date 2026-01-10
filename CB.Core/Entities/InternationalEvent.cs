
namespace CB.Core.Entities
{
    public class InternationalEvent : BaseEntity
    {
        public List<InternationalEventTranslation> Translations { get; set; } = new();
        public List<InternationalEventImage> Images { get; set; } = new();
    }
}
