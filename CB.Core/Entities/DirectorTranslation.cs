
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DirectorTranslation : BaseEntity
    {
        [StringLength(100)]
        public string? Fullname { get; set; }
        [StringLength(100)]
        public string? Slug { get; set; }
        [StringLength(200)]
        public string? Position { get; set; }
        public int DirectorId { get; set; }
        public Director? Director { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
