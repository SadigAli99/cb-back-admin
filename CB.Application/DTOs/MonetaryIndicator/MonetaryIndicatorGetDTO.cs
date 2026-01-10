
namespace CB.Application.DTOs.MonetaryIndicator
{
    public class MonetaryIndicatorGetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string? PercentCategoryTitle { get; set; }
    }
}
