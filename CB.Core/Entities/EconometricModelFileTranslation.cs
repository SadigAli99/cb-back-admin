

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class EconometricModelFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int EconometricModelFileId { get; set; }
        public EconometricModelFile? EconometricModelFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
