
namespace CB.Core.Entities
{
    public class MeasTranslation : BaseEntity
    {
        public string? Title { get; set; }
        public int MeasId { get; set; }
        public Meas Meas { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
