

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ExecutionStatusTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ExecutionStatusId { get; set; }
        public ExecutionStatus ExecutionStatus { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
