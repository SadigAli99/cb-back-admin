
namespace CB.Application.DTOs.BankNote
{
    public class BankNoteCreateDTO
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public double PercentValue { get; set; }
        public int BankNoteCategoryId { get; set; }
    }
}
