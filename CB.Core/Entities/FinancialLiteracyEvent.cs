
namespace CB.Core.Entities
{
    public class FinancialLiteracyEvent : BaseEntity
    {
        public DateTime Date { get; set; }
        public List<FinancialLiteracyEventTranslation> Translations { get; set; } = new();
    }
}
