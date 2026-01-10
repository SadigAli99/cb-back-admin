
namespace CB.Core.Entities
{
    public class IndexPeriod : BaseEntity
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int IndexPeriodCategoryId { get; set; }
        public IndexPeriodCategory? IndexPeriodCategory { get; set; }
    }
}
