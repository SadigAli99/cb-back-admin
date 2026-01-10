

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ComplaintIndexTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int ComplaintIndexId { get; set; }
        public ComplaintIndex? ComplaintIndex { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
