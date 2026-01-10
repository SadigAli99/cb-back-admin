
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Phone : BaseEntity
    {
        [StringLength(100)]
        public string? ContactNumber { get; set; }
        public List<PhoneTranslation>? Translations { get; set; }
    }
}
