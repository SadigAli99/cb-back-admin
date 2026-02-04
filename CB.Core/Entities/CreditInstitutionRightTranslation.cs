

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditInstitutionRightTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CreditInstitutionRightId { get; set; }
        public CreditInstitutionRight? CreditInstitutionRight { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
