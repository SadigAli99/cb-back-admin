
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VirtualActiveTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int VirtualActiveId { get; set; }
        public VirtualActive VirtualActive { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
