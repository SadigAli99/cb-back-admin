
namespace CB.Core.Entities
{
    public class YieldParameter : BaseEntity
    {
        public DateTime Date { get; set; }
        public double BetaZero { get; set; }
        public double BetaOne { get; set; }
        public double BetaTwo { get; set; }
        public double Lambda { get; set; }
    }
}
