
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsuranceBroker : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<InsuranceBrokerTranslation> Translations { get; set; } = new();
    }
}
