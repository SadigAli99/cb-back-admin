
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CurrencyHistoryNextItem : BaseEntity
    {
        [StringLength(100)]
        public string? FrontImage { get; set; }
        [StringLength(100)]
        public string? BackImage { get; set; }
        public int CurrencyHistoryNextId { get; set; }
        public CurrencyHistoryNext CurrencyHistoryNext { get; set; } = null!;
    }
}
