

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InformationIssuerTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InformationIssuerId { get; set; }
        public InformationIssuer? InformationIssuer { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
