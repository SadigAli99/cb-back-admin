

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InvestmentCompanyFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InvestmentCompanyFileId { get; set; }
        public InvestmentCompanyFile? InvestmentCompanyFile { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
