
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OpenBankingTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int OpenBankingId { get; set; }
        public OpenBanking OpenBanking { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
