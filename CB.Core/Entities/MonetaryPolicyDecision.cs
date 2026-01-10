
namespace CB.Core.Entities
{
    public class MonetaryPolicyDecision : BaseEntity
    {
        public int Year { get; set; }
        public List<MonetaryPolicyDecisionTranslation> Translations { get; set; } = new();
    }
}
