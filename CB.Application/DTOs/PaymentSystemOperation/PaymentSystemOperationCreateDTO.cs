

namespace CB.Application.DTOs.PaymentSystemOperation
{
    public class PaymentSystemOperationCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
