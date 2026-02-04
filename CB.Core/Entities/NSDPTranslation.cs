

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NSDPTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int NSDPId { get; set; }
        public NSDP? NSDP { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
