

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PostalCommunicationTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PostalCommunicationId { get; set; }
        public PostalCommunication? PostalCommunication { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
