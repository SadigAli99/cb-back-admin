

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReviewApplicationFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ReviewApplicationFileId { get; set; }
        public ReviewApplicationFile? ReviewApplicationFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
