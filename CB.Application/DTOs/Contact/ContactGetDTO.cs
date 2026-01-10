
namespace CB.Application.DTOs.Contact
{
    public class ContactGetDTO
    {
        public string? ContactMail { get; set; }
        public string? FileSize { get; set; }
        public string? ReceptionSchedule { get; set; }
        public string? Map { get; set; }
        public Dictionary<string, string> Notes { get; set; } = new();
        public Dictionary<string, string> RegistrationTimes { get; set; } = new();
    }

}
