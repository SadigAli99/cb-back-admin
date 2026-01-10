
namespace CB.Application.DTOs.PaymentSystemControlService
{
    public class PaymentSystemControlServiceGetDTO
    {
        public int Id { get; set; }
        public string? PaymentSystemControlTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
