
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerDocument : BaseEntity
    {
        public List<CustomerDocumentTranslation> Translations { get; set; } = new();
    }
}
