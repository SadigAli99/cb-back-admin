
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Participant : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int ParticipantCategoryId { get; set; }
        public ParticipantCategory ParticipantCategory { get; set; } = null!;
        public List<ParticipantTranslation> Translations { get; set; } = new();
    }
}
