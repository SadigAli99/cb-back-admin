

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class SoftwareTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int SoftwareId { get; set; }
        public Software? Software { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
