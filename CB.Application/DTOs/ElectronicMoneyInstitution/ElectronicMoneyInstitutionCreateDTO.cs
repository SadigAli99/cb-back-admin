

namespace CB.Application.DTOs.ElectronicMoneyInstitution
{
    public class ElectronicMoneyInstitutionCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
