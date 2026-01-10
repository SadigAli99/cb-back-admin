using CB.Application.DTOs.LotteryVideo;

namespace CB.Application.Interfaces.Services
{
    public interface ILotteryVideoService
    {
        Task<List<LotteryVideoGetDTO>> GetAllAsync(int? id);
        Task<LotteryVideoGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LotteryVideoCreateDTO dto);
        Task<bool> UpdateAsync(int id, LotteryVideoEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
