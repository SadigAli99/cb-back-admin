
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StaffArticleFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int StaffArticleId { get; set; }
        public StaffArticle StaffArticle { get; set; } = null!;
        public List<StaffArticleFileTranslation> Translations { get; set; } = new();
    }
}
