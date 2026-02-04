

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentInstitutionFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PaymentInstitutionFileId { get; set; }
        public PaymentInstitutionFile? PaymentInstitutionFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
