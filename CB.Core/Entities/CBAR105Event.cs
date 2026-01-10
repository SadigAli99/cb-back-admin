
namespace CB.Core.Entities
{
    public class CBAR105Event : BaseEntity
    {
        public List<CBAR105EventTranslation> Translations { get; set; } = new();
        public List<CBAR105EventImage> Images { get; set; } = new();
    }
}
