
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LotteryVideoTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int LotteryVideoId { get; set; }
        public LotteryVideo LotteryVideo { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
