

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentSystemControlFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PaymentSystemControlFileId { get; set; }
        public PaymentSystemControlFile? PaymentSystemControlFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
