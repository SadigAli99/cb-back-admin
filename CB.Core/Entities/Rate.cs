namespace CB.Core.Entities
{
    public class Rate : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Index { get; set; }
        public int ValuteId { get; set; }
        public Valute? Valute { get; set; }
    }
}
