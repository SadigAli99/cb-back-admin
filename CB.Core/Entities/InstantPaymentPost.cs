
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InstantPaymentPost : BaseEntity
    {
        public List<InstantPaymentPostTranslation> Translations { get; set; } = new();
    }
}
