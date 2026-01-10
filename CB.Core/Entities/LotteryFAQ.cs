
namespace CB.Core.Entities
{
    public class LotteryFAQ : BaseEntity
    {
        public int LotteryId { get; set; }
        public Lottery Lottery { get; set; } = null!;
        public List<LotteryFAQTranslation> Translations { get; set; } = new();
    }
}
