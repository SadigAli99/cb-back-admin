
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialSearchSystemImage : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public int FinancialSearchSystemId { get; set; }
        public FinancialSearchSystem FinancialSearchSystem { get; set; } = null!;
    }
}
