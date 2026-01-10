
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LotteryVideo : BaseEntity
    {
        [StringLength(1000)]
        public string? Url { get; set; }
        public int LotteryId { get; set; }
        public Lottery Lottery { get; set; } = null!;
        public List<LotteryVideoTranslation> Translations { get; set; } = new();
    }
}
