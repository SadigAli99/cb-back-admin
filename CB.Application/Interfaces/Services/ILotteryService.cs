
using CB.Application.DTOs.Lottery;

namespace CB.Application.Interfaces.Services
{
    public interface ILotteryService
    {
        Task<List<LotteryGetDTO>> GetAllAsync();
        Task<LotteryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LotteryCreateDTO dto);
        Task<bool> UpdateAsync(int id, LotteryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
