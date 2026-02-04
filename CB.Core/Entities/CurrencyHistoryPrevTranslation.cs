

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryPrevTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? SubTitle { get; set; }
        public int CurrencyHistoryPrevId { get; set; }
        public CurrencyHistoryPrev? CurrencyHistoryPrev { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
