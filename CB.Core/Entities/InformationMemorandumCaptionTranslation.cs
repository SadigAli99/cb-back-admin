
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InformationMemorandumCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InformationMemorandumCaptionId { get; set; }
        public InformationMemorandumCaption InformationMemorandumCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
