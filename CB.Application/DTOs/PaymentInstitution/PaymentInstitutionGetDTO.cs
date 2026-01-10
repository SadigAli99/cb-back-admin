
namespace CB.Application.DTOs.PaymentInstitution
{
    public class PaymentInstitutionGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
