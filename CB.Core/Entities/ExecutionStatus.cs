
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ExecutionStatus : BaseEntity
    {
        public int Year { get; set; }
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<ExecutionStatusTranslation> Translations { get; set; } = new();
    }
}
