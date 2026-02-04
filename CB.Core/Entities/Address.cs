
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Address : BaseEntity
    {
        public bool IsMain { get; set; }
        [StringLength(10000)]
        public string? Map { get; set; }
        public List<AddressTranslation>Translations {get; set; } = new();
    }
}
