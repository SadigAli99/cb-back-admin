
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Translate : BaseEntity
    {
        [StringLength(255)]
        public string Key { get; set; } = null!;
        public List<TranslateTranslation>? Translations { get; set; }
    }
}
