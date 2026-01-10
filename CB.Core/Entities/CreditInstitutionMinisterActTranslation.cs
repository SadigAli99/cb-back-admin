

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditInstitutionMinisterActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CreditInstitutionMinisterActId { get; set; }
        public CreditInstitutionMinisterAct? CreditInstitutionMinisterAct { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
