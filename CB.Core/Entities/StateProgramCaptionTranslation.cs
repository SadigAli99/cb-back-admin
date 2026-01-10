
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StateProgramCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int StateProgramCaptionId { get; set; }
        public StateProgramCaption StateProgramCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
