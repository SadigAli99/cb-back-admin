
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PositionTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
