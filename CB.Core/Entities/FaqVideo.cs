

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FaqVideo : BaseEntity
    {
        [StringLength(1000)]
        public string? VideoUrl { get; set; }
        [StringLength(1000)]
        public string? PlaylistUrl { get; set; }
        public List<FaqVideoTranslation>Translations {get; set; } = new();
    }
}
