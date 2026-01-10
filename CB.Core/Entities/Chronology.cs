

namespace CB.Core.Entities
{
    public class Chronology : BaseEntity
    {
        public int Year { get; set; }
        public List<ChronologyTranslation>? Translations { get; set; }
    }
}
