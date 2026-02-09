

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InformationMemorandumTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InformationMemorandumId { get; set; }
        public InformationMemorandum? InformationMemorandum { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
