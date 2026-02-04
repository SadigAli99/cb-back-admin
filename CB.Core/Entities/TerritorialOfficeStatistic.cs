
namespace CB.Core.Entities
{
    public class TerritorialOfficeStatistic : BaseEntity
    {
        public int? TerritorialOfficeId { get; set; }
        public TerritorialOffice TerritorialOffice { get; set; } = null!;
        public List<TerritorialOfficeStatisticTranslation> Translations { get; set; } = new();
    }
}
