
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReceptionCitizenLinkTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ReceptionCitizenLinkId { get; set; }
        public ReceptionCitizenLink ReceptionCitizenLink { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
