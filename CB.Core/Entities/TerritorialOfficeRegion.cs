
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TerritorialOfficeRegion : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public int TerritorialOfficeId { get; set; }
        public TerritorialOffice TerritorialOffice { get; set; } = null!;
        public List<TerritorialOfficeRegionTranslation> Translations { get; set; } = new();
    }
}
