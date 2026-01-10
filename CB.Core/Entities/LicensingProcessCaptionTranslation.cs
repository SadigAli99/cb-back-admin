
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LicensingProcessCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int LicensingProcessCaptionId { get; set; }
        public LicensingProcessCaption LicensingProcessCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
