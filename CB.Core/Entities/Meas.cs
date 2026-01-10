
namespace CB.Core.Entities
{
    public class Meas : BaseEntity
    {
        public DateTime RegisterTime { get; set; }
        public string? RegisterNumber { get; set; }
        public string? PdfFile { get; set; }
        public int IssuerTypeId { get; set; }
        public IssuerType IssuerType { get; set; } = null!;
        public int InformationTypeId { get; set; }
        public InformationType InformationType { get; set; } = null!;
        public int SecurityTypeId { get; set; }
        public SecurityType SecurityType { get; set; } = null!;
        public List<MeasTranslation> Translations { get; set; } = new();
    }
}
