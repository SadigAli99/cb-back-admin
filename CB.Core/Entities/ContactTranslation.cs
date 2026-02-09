
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ContactTranslation : BaseEntity
    {
        [StringLength(2000)]
        public string? Note { get; set; }
        [StringLength(200)]
        public string? RegistrationTime { get; set; }
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
