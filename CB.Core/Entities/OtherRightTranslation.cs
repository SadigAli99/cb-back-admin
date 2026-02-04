

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherRightTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int OtherRightId { get; set; }
        public OtherRight? OtherRight { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
