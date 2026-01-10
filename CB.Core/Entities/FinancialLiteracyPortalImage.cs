
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FinancialLiteracyPortalImage : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public int FinancialLiteracyPortalId { get; set; }
        public FinancialLiteracyPortal FinancialLiteracyPortal { get; set; } = null!;
    }
}
