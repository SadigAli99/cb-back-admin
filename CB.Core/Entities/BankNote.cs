
namespace CB.Core.Entities
{
    public class BankNote : BaseEntity
    {
        public DateTime Date { get; set; }
        public double? Value { get; set; } = null;
        public double? PercentValue { get; set; } = null;
        public int BankNoteCategoryId { get; set; }

        public BankNoteCategory? BankNoteCategory { get; set; }
    }
}
