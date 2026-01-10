
namespace CB.Core.Entities
{
    public class Advertisement : BaseEntity
    {
        public DateTime Date { get; set; }
        public List<AdvertisementTranslation> Translations { get; set; } = new();
    }
}
