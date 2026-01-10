
namespace CB.Application.DTOs.MonetaryIndicator
{
    public class MonetaryIndicatorCreateDTO
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int MonetaryIndicatorCategoryId { get; set; }
    }
}
