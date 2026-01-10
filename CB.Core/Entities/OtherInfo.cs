
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherInfo : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        public List<OtherInfoTranslation> Translations { get; set; } = new();
    }
}
