
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternationalFinancialOrganizationTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InternationalFinancialOrganizationId { get; set; }
        public InternationalFinancialOrganization InternationalFinancialOrganization { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
