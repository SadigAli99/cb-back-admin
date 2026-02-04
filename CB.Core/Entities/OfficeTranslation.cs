

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OfficeTranslation : BaseEntity
    {
        [StringLength(100)]
        public string? Title { get; set; }
        [StringLength(100)]
        public string? Chairman { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public int OfficeId { get; set; }
        public Office? Office { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
