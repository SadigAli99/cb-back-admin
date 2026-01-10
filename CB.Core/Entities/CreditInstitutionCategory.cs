
namespace CB.Core.Entities
{
    public class CreditInstitutionCategory : BaseEntity
    {
        public List<CreditInstitutionCategoryTranslation> Translations { get; set; } = new();
    }
}
