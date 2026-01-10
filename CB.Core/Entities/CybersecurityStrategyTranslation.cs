

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CybersecurityStrategyTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int CybersecurityStrategyId { get; set; }
        public CybersecurityStrategy? CybersecurityStrategy { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
