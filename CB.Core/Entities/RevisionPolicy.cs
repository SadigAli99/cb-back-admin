

namespace CB.Core.Entities
{
    public class RevisionPolicy : BaseEntity
    {
        public List<RevisionPolicyTranslation>Translations {get; set; } = new();
    }
}
