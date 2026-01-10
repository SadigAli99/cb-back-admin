
namespace CB.Core.Entities
{
    public class MarketPercentDegree : BaseEntity
    {
        public DateTime Date { get; set; }
        public string? PercentValue { get; set; }
        public double? PercentVolume { get; set; }
        public int? DealCount { get; set; }
    }
}
