

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LotteryTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int LotteryId { get; set; }
        public Lottery? Lottery { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
