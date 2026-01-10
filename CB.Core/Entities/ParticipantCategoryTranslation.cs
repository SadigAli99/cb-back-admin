

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ParticipantCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int ParticipantCategoryId { get; set; }
        public ParticipantCategory ParticipantCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
