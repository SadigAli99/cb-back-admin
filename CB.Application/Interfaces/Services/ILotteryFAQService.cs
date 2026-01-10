
using CB.Application.DTOs.LotteryFAQ;

namespace CB.Application.Interfaces.Services
{
    public interface ILotteryFAQService
    {
        Task<List<LotteryFAQGetDTO>> GetAllAsync(int? id);
        Task<LotteryFAQGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LotteryFAQCreateDTO dto);
        Task<bool> UpdateAsync(int id, LotteryFAQEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
