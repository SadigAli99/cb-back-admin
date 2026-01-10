
namespace CB.Core.Entities
{
    public class InfographicDisclosureCategory : BaseEntity
    {
        public List<InfographicDisclosureCategoryTranslation> Translations { get; set; } = new();
    }
}
