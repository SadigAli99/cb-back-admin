
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ManagerContactTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ManagerContactId { get; set; }
        public ManagerContact? ManagerContact { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
