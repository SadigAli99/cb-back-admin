
namespace CB.Application.DTOs.ReceptionCitizen
{
    public class ReceptionCitizenGetDTO
    {
        public int Id { get; set; }
        public string? ReceptionCitizenCategory { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
