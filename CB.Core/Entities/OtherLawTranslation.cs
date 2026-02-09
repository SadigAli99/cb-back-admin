

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherLawTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int OtherLawId { get; set; }
        public OtherLaw? OtherLaw { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
