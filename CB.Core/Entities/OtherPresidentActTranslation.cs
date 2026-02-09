

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherPresidentActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int OtherPresidentActId { get; set; }
        public OtherPresidentAct? OtherPresidentAct { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
