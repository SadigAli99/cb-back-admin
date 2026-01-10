

namespace CB.Core.Entities
{
    public class CustomerFeedbackCaption : BaseEntity
    {
        public List<CustomerFeedbackCaptionTranslation> Translations { get; set; } = new();
    }
}
