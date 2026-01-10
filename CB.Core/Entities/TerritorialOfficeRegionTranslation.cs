

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TerritorialOfficeRegionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Director { get; set; }
        [StringLength(500)]
        public string? Location { get; set; }
        public int TerritorialOfficeRegionId { get; set; }
        public TerritorialOfficeRegion TerritorialOfficeRegion { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
