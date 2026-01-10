
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReceptionCitizenLink : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        public int ReceptionCitizenCategoryId { get; set; }
        public ReceptionCitizenCategory ReceptionCitizenCategory { get; set; } = null!;
        public List<ReceptionCitizenLinkTranslation> Translations { get; set; } = new();
    }
}
