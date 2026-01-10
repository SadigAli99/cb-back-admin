
namespace CB.Core.Entities
{
    public class TerritorialOfficeStatistic : BaseEntity
    {
        public int? TerritorialOfficeId { get; set; }
        public TerritorialOffice? TerritorialOffice { get; set; }
        public List<TerritorialOfficeStatisticTranslation> Translations { get; set; } = new();
    }
}
