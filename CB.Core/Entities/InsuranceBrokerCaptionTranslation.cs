
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceBrokerCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InsuranceBrokerCaptionId { get; set; }
        public InsuranceBrokerCaption InsuranceBrokerCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
