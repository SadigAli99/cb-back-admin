
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CentralBankCooperationCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CentralBankCooperationCaptionId { get; set; }
        public CentralBankCooperationCaption CentralBankCooperationCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
