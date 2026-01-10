
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InfographicDisclosureGraphicTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InfographicDisclosureGraphicId { get; set; }
        public InfographicDisclosureGraphic InfographicDisclosureGraphic { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
