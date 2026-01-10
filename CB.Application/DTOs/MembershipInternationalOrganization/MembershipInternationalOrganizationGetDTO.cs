
namespace CB.Application.DTOs.MembershipInternationalOrganization
{
    public class MembershipInternationalOrganizationGetDTO
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
