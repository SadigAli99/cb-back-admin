

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TerritorialOfficeTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int TerritorialOfficeId { get; set; }
        public TerritorialOffice TerritorialOffice { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
