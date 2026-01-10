

namespace CB.Core.Entities
{
    public class FormerChairman : BaseEntity
    {
        public int Year { get; set; }
        public List<FormerChairmanTranslation>? Translations { get; set; }
    }
}
