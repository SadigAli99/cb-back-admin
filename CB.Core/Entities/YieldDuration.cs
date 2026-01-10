
namespace CB.Core.Entities
{
    public class YieldDuration : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Index { get; set; }
        public int DurationId { get; set; }
        public Duration? Duration { get; set; }
    }
}
