
namespace CB.Core.Entities
{
    public class CreditInstitutionSubCategory : BaseEntity
    {
        public int? CreditInstitutionCategoryId { get; set; }
        public CreditInstitutionCategory? CreditInstitutionCategory { get; set; }
        public List<CreditInstitutionSubCategoryTranslation> Translations { get; set; } = new();
    }
}
