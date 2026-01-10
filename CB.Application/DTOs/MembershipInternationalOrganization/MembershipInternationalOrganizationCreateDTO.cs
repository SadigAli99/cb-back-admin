
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MembershipInternationalOrganization
{
    public class MembershipInternationalOrganizationCreateDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
