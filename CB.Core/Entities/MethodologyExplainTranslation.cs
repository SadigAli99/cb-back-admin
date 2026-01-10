
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MethodologyExplainTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MethodologyExplainId { get; set; }
        public MethodologyExplain MethodologyExplain { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
