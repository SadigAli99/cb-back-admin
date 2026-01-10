
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditInstitution : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int? CreditInstitutionCategoryId { get; set; }
        public CreditInstitutionCategory? CreditInstitutionCategory { get; set; }
        public int? CreditInstitutionSubCategoryId { get; set; }
        public CreditInstitutionSubCategory? CreditInstitutionSubCategory { get; set; }
        public List<CreditInstitutionTranslation> Translations { get; set; } = new();
    }
}
