
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LicensingProcess : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<LicensingProcessTranslation> Translations { get; set; } = new();
    }
}
