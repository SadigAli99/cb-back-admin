
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Statute : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; }
        public List<StatuteTranslation> Translations { get; set; } = new();
    }
}
