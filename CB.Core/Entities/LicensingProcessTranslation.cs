

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LicensingProcessTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int LicensingProcessId { get; set; }
        public LicensingProcess? LicensingProcess { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
