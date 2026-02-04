

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StructureCaption : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<StructureCaptionTranslation>Translations {get; set; } = new();
    }
}
