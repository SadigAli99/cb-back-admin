

namespace CB.Core.Entities
{
    public class ProcessingActivity : BaseEntity
    {
        public List<ProcessingActivityTranslation> Translations { get; set; } = new();
    }
}
