

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StateProgramTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int StateProgramId { get; set; }
        public StateProgram? StateProgram { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
