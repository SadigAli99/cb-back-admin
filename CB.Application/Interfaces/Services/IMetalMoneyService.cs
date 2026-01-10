
using CB.Application.DTOs.MetalMoney;

namespace CB.Application.Interfaces.Services
{
    public interface IMetalMoneyService
    {
        Task<List<MetalMoneyGetDTO>> GetAllAsync();
        Task<MetalMoneyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MetalMoneyCreateDTO dto);
        Task<bool> UpdateAsync(int id, MetalMoneyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
