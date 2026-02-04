

namespace CB.Core.Entities
{
    public class Manager : BaseEntity
    {
        public string? Image { get; set; }
        public List<ManagerTranslation> Translations { get; set; } = new();
        public List<ManagerContact>? Contacts { get; set; }
        public List<ManagerDetail>? Details { get; set; }
    }
}
