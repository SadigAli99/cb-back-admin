
namespace CB.Core.Entities
{
    public class CustomerEvent : BaseEntity
    {
        public List<CustomerEventTranslation> Translations { get; set; } = new();
        public List<CustomerEventImage>? Images { get; set; } = new();
    }
}
