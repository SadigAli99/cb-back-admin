

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PaymentSystemStandartFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PaymentSystemStandartFileId { get; set; }
        public PaymentSystemStandartFile PaymentSystemStandartFile { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
