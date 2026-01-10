

namespace CB.Core.Entities
{
    public class CreditUnion : BaseEntity
    {
        public List<CreditUnionTranslation> Translations { get; set; } = new();
    }
}
