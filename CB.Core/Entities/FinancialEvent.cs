
namespace CB.Core.Entities
{
    public class FinancialEvent : BaseEntity
    {
        public DateTime Date { get; set; }
        public List<FinancialEventTranslation> Translations { get; set; } = new();
    }
}
