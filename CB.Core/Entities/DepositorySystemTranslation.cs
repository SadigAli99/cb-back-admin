
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DepositorySystemTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int DepositorySystemId { get; set; }
        public DepositorySystem DepositorySystem { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
