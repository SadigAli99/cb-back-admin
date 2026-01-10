
namespace CB.Application.DTOs.InstantPaymentOrganization
{
    public class InstantPaymentOrganizationGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
