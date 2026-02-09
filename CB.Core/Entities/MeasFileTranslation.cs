

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MeasFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int MeasFileId { get; set; }
        public MeasFile? MeasFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
