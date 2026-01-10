
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBAR100VideoTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CBAR100VideoId { get; set; }
        public CBAR100Video CBAR100Video { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
