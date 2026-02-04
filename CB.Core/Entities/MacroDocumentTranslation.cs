

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MacroDocumentTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Text { get; set; }
        public int MacroDocumentId { get; set; }
        public MacroDocument? MacroDocument { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
