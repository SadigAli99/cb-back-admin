

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditInstitutionLawTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CreditInstitutionLawId { get; set; }
        public CreditInstitutionLaw? CreditInstitutionLaw { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
