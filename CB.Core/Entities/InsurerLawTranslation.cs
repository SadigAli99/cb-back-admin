

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerLawTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int InsurerLawId { get; set; }
        public InsurerLaw? InsurerLaw { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
