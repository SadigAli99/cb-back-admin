
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBDCTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CBDCId { get; set; }
        public CBDC CBDC { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
