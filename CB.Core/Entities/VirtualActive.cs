

namespace CB.Core.Entities
{
    public class VirtualActive : BaseEntity
    {
        public List<VirtualActiveTranslation>Translations {get; set; } = new();
    }
}
