

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerPresidentActTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int InsurerPresidentActId { get; set; }
        public InsurerPresidentAct? InsurerPresidentAct { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
