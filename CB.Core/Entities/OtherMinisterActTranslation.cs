

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherMinisterActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int OtherMinisterActId { get; set; }
        public OtherMinisterAct? OtherMinisterAct { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
