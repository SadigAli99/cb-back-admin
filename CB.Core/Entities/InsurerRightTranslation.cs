

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerRightTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int InsurerRightId { get; set; }
        public InsurerRight? InsurerRight { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
