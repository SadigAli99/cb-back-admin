
using CB.Application.DTOs.CapitalMarketFile;

namespace CB.Application.Interfaces.Services
{
    public interface ICapitalMarketFileService
    {
        Task<List<CapitalMarketFileGetDTO>> GetAllAsync();
        Task<CapitalMarketFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CapitalMarketFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, CapitalMarketFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
