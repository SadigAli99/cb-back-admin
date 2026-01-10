

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NakhchivanPublicationTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int NakhchivanPublicationId { get; set; }
        public NakhchivanPublication? NakhchivanPublication { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
