
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PageDetailTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; }
        [StringLength(255)]
        public string? Url { get; set; }
        [StringLength(255)]
        public string? MetaTitle { get; set; }
        [StringLength(255)]
        public string? MetaDescription { get; set; }
        public int PageDetailId { get; set; }
        public PageDetail PageDetail { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
