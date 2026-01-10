
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Mission : BaseEntity
    {
        [StringLength(100)]
        public string? Icon { get; set; }
        public List<MissionTranslation> Translations { get; set; } = new();
    }
}
