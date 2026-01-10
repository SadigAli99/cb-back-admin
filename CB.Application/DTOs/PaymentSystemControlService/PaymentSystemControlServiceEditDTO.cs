

namespace CB.Application.DTOs.PaymentSystemControlService
{
    public class PaymentSystemControlServiceEditDTO
    {
        public int Id { get; set; }
        public int PaymentSystemControlId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
