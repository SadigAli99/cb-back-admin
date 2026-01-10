
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternshipDirection : BaseEntity
    {
        [StringLength(100)]
        public string? Icon { get; set; }
        public List<InternshipDirectionTranslation> Translations { get; set; } = new();
    }
}
