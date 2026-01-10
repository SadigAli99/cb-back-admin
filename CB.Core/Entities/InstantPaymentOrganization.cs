
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InstantPaymentOrganization : BaseEntity
    {
        public List<InstantPaymentOrganizationTranslation> Translations { get; set; } = new();
    }
}
