
using CB.Application.DTOs.PolicyAnalysisFile;

namespace CB.Application.Interfaces.Services
{
    public interface IPolicyAnalysisFileService
    {
        Task<List<PolicyAnalysisFileGetDTO>> GetAllAsync();
        Task<PolicyAnalysisFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PolicyAnalysisFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, PolicyAnalysisFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
