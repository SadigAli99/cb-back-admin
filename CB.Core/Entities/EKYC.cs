

namespace CB.Core.Entities
{
    public class EKYC : BaseEntity
    {
        public List<EKYCTranslation>Translations {get; set; } = new();
    }
}
