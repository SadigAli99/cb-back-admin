
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BranchTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
