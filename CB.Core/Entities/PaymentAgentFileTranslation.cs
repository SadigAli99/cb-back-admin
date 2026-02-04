

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentAgentFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PaymentAgentFileId { get; set; }
        public PaymentAgentFile? PaymentAgentFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
