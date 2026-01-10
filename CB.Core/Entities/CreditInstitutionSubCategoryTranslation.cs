
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditInstitutionSubCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int CreditInstitutionSubCategoryId { get; set; }
        public CreditInstitutionSubCategory CreditInstitutionSubCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
