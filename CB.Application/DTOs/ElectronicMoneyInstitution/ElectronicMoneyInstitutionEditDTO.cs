

namespace CB.Application.DTOs.ElectronicMoneyInstitution
{
    public class ElectronicMoneyInstitutionEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
