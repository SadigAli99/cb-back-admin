

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BlogTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(255)]
        public string? ImageTitle { get; set; }
        [StringLength(255)]
        public string? ImageAlt { get; set; }
        [StringLength(255)]
        public string? MetaTitle { get; set; }
        [StringLength(255)]
        public string? MetaDescription { get; set; }
        [StringLength(255)]
        public string? MetaKeyword { get; set; }
        [StringLength(1000)]
        public string? ShortDescription { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
