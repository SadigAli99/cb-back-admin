

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FutureEventTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Location { get; set; }
        [StringLength(100)]
        public string? Format { get; set; }
        public int FutureEventId { get; set; }
        public FutureEvent? FutureEvent { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
