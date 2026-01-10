
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentSecurity : BaseEntity
    {
        public List<InvestmentSecurityTranslation> Translations { get; set; } = new();
        public List<InvestmentSecurityFile> InvestmentSecurityFiles { get; set; } = new();
    }
}
