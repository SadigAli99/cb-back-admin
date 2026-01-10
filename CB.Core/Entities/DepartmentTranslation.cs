
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DepartmentTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
