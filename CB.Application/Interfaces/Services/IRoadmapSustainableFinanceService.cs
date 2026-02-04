
using CB.Application.DTOs.RoadmapSustainableFinance;

namespace CB.Application.Interfaces.Services
{
    public interface IRoadmapSustainableFinanceService
    {
        Task<bool> CreateOrUpdate(RoadmapSustainableFinancePostDTO dTO);
        Task<RoadmapSustainableFinanceGetDTO?> GetFirst();
    }
}
