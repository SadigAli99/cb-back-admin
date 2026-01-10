
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TranslateTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Value { get; set; } = null;

        public int TranslateId { get; set; }
        public Translate Translate { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
