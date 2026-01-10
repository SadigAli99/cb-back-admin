
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AddressTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Text { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
