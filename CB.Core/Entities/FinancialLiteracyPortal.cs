
namespace CB.Core.Entities
{
    public class FinancialLiteracyPortal : BaseEntity
    {
        public List<FinancialLiteracyPortalTranslation> Translations { get; set; } = new();
        public List<FinancialLiteracyPortalImage> Images { get; set; } = new();
    }
}
