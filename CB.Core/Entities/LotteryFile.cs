
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LotteryFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int LotteryId { get; set; }
        public Lottery Lottery { get; set; } = null!;
        public List<LotteryFileTranslation> Translations { get; set; } = new();
    }
}
