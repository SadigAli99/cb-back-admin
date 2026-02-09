

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StaffArticleFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int StaffArticleFileId { get; set; }
        public StaffArticleFile? StaffArticleFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
