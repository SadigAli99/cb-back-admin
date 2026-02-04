

namespace CB.Core.Entities
{
    public class CustomerRightCaption : BaseEntity
    {
        public List<CustomerRightCaptionTranslation>Translations {get; set; } = new();
    }
}
