
namespace CB.Application.DTOs.BankNote
{
    public class BankNoteGetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public double PercentValue { get; set; }
        public string? PercentCategoryTitle { get; set; }
    }
}
