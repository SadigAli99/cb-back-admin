
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MacroDocument : BaseEntity
    {
        [StringLength(100)]
        public string? Icon { get; set; }
        [StringLength(500)]
        public string? Url { get; set; }
        public List<MacroDocumentTranslation> Translations { get; set; } = new();
    }
}
