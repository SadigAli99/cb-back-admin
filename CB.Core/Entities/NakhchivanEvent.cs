
namespace CB.Core.Entities
{
    public class NakhchivanEvent : BaseEntity
    {
        public List<NakhchivanEventTranslation> Translations { get; set; } = new();
        public List<NakhchivanEventImage>? Images { get; set; } = new();
    }
}
