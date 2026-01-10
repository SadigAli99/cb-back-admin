
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MediaQueryTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int MediaQueryId { get; set; }
        public MediaQuery MediaQuery { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
