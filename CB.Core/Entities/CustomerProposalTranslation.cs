
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerProposalTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CustomerProposalId { get; set; }
        public CustomerProposal CustomerProposal { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
