
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialInstitutionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FinancialInstitutionId { get; set; }
        public FinancialInstitution FinancialInstitution { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
