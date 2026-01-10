
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerContact : BaseEntity
    {
        [StringLength(500)]
        public string? Map { get; set; }
        public List<CustomerContactTranslation> Translations { get; set; } = new();
    }
}
