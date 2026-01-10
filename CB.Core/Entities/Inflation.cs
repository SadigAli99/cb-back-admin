
namespace CB.Core.Entities
{
    public class Inflation : BaseEntity
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double Value { get; set; }
    }
}
