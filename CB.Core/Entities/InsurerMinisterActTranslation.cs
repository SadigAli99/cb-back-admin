

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerMinisterActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int InsurerMinisterActId { get; set; }
        public InsurerMinisterAct? InsurerMinisterAct { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
