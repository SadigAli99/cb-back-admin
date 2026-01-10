
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StructureCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int StructureCaptionId { get; set; }
        public StructureCaption StructureCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
