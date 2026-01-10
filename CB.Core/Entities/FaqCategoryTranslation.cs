
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FaqCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int FaqCategoryId { get; set; }
        public FaqCategory FaqCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
