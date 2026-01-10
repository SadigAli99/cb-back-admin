
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FaqVideoTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int FaqVideoId { get; set; }
        public FaqVideo FaqVideo { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
