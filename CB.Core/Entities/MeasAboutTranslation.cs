
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MeasAboutTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MeasAboutId { get; set; }
        public MeasAbout MeasAbout { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
