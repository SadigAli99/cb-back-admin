

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceBrokerTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InsuranceBrokerId { get; set; }
        public InsuranceBroker? InsuranceBroker { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
