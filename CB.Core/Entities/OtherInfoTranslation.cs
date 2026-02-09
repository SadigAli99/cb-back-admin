

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherInfoTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int OtherInfoId { get; set; }
        public OtherInfo? OtherInfo { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
