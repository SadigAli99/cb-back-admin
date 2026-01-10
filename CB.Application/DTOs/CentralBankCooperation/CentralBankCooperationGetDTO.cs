
namespace CB.Application.DTOs.CentralBankCooperation
{
    public class CentralBankCooperationGetDTO
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
