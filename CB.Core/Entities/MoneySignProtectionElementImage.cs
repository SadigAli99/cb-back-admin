
namespace CB.Core.Entities
{
    public class MoneySignProtectionElementImage : BaseEntity
    {
        public string? Image { get; set; }
        public int MoneySignProtectionElementId { get; set; }
        public MoneySignProtectionElement MoneySignProtectionElement { get; set; } = null!;
    }
}
