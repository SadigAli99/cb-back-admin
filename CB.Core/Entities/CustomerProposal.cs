

namespace CB.Core.Entities
{
    public class CustomerProposal : BaseEntity
    {
        public List<CustomerProposalTranslation> Translations { get; set; } = new();
    }
}
