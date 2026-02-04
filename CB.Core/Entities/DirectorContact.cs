
namespace CB.Core.Entities
{
    public class DirectorContact : BaseEntity
    {
        public int DirectorId { get; set; }
        public Director Director { get; set; } = null!;
        public List<DirectorContactTranslation>Translations {get; set; } = new();
    }
}
