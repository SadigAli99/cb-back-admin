
namespace CB.Core.Entities
{
    public class YieldCurve : BaseEntity
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public double Index { get; set; }
    }
}
