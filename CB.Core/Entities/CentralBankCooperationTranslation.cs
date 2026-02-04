

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CentralBankCooperationTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CentralBankCooperationId { get; set; }
        public CentralBankCooperation? CentralBankCooperation { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
