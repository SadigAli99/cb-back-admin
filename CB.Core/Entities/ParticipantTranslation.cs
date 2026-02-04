

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ParticipantTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
