
namespace CB.Application.DTOs.BankSector
{
    public class BankSectorGetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string? PercentCategoryTitle { get; set; }
    }
}
