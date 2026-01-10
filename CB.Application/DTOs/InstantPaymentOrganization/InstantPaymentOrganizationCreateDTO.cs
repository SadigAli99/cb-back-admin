

namespace CB.Application.DTOs.InstantPaymentOrganization
{
    public class InstantPaymentOrganizationCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
