

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBAR105EventTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CBAR105EventId { get; set; }
        public CBAR105Event? CBAR105Event { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
