
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StateProgram : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int StateProgramCategoryId { get; set; }
        public StateProgramCategory StateProgramCategory { get; set; } = null!;
        public List<StateProgramTranslation> Translations { get; set; } = new();
    }
}
