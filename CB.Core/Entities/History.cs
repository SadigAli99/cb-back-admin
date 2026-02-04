

namespace CB.Core.Entities
{
    public class History : BaseEntity
    {
        public List<HistoryTranslation>Translations {get; set; } = new();
    }
}
