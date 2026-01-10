
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReceptionCitizenCategoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ReceptionCitizenCategoryId { get; set; }
        public ReceptionCitizenCategory ReceptionCitizenCategory { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
