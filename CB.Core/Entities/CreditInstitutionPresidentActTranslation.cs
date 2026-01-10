

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditInstitutionPresidentActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CreditInstitutionPresidentActId { get; set; }
        public CreditInstitutionPresidentAct? CreditInstitutionPresidentAct { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
