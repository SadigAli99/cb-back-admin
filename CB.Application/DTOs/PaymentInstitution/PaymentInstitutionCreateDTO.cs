

namespace CB.Application.DTOs.PaymentInstitution
{
    public class PaymentInstitutionCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
