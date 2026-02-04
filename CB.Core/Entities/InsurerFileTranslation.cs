

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InsurerFileId { get; set; }
        public InsurerFile? InsurerFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
