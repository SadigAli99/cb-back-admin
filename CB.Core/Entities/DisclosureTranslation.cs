

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DisclosureTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int DisclosureId { get; set; }
        public Disclosure? Disclosure { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
