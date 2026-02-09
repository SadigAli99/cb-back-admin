

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternshipDirectionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(20000)]
        public string? Description { get; set; }
        public int InternshipDirectionId { get; set; }
        public InternshipDirection? InternshipDirection { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
