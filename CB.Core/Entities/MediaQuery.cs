

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MediaQuery : BaseEntity
    {
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(100)]
        public string? Phone { get; set; }
        public List<MediaQueryTranslation> Translations { get; set; } = new();
    }
}
