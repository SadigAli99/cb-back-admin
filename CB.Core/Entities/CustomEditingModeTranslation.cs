
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomEditingModeTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CustomEditingModeId { get; set; }
        public CustomEditingMode CustomEditingMode { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
