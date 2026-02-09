

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CreditUnionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CreditUnionId { get; set; }
        public CreditUnion? CreditUnion { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
