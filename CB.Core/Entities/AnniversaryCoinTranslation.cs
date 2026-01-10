
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AnniversaryCoinTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int AnniversaryCoinId { get; set; }
        public AnniversaryCoin AnniversaryCoin { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
