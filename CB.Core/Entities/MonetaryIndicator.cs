
namespace CB.Core.Entities
{
    public class MonetaryIndicator : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int MonetaryIndicatorCategoryId { get; set; }

        public MonetaryIndicatorCategory? MonetaryIndicatorCategory { get; set; }
    }
}
