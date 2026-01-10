
using CB.Application.DTOs.RoadmapOverview;

namespace CB.Application.Interfaces.Services
{
    public interface IRoadmapOverviewService
    {
        Task<List<RoadmapOverviewGetDTO>> GetAllAsync();
        Task<RoadmapOverviewGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RoadmapOverviewCreateDTO dto);
        Task<bool> UpdateAsync(int id, RoadmapOverviewEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
