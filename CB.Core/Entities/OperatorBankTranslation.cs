
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OperatorBankTranslation : BaseEntity
    {
        [StringLength(50)]
        public string? Title { get; set; }
        public int OperatorBankId { get; set; }
        public OperatorBank? OperatorBank { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
