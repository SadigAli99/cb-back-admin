
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PhoneTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
