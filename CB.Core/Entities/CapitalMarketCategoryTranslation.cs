
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CapitalMarketCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int CapitalMarketCategoryId { get; set; }
        public CapitalMarketCategory CapitalMarketCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
