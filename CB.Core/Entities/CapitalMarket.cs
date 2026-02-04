
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarket : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int? CapitalMarketCategoryId { get; set; }
        public CapitalMarketCategory CapitalMarketCategory { get; set; } = null!;
        public List<CapitalMarketTranslation> Translations { get; set; } = new();
    }
}
