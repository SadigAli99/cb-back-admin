
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ManagerDetailTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ManagerDetailId { get; set; }
        public ManagerDetail? ManagerDetail { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
