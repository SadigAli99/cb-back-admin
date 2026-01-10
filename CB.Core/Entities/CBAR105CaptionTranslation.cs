
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBAR105CaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CBAR105CaptionId { get; set; }
        public CBAR105Caption CBAR105Caption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
