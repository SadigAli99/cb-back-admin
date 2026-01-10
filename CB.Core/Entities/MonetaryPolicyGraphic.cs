

namespace CB.Core.Entities
{
    public class MonetaryPolicyGraphic : BaseEntity
    {
        public List<MonetaryPolicyGraphicTranslation> Translations { get; set; } = new();
    }
}
