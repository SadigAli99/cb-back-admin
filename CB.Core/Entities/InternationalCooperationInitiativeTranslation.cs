

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternationalCooperationInitiativeTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InternationalCooperationInitiativeId { get; set; }
        public InternationalCooperationInitiative? InternationalCooperationInitiative { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
