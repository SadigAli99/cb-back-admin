

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VirtualEducationCaption : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<VirtualEducationCaptionTranslation>Translations {get; set; } = new();
    }
}
