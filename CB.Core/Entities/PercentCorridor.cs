
namespace CB.Core.Entities
{
    public class PercentCorridor : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int PercentCorridorCategoryId { get; set; }

        public PercentCorridorCategory? PercentCorridorCategory { get; set; }
    }
}
