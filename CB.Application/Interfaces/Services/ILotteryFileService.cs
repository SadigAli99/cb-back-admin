
using CB.Application.DTOs.LotteryFile;

namespace CB.Application.Interfaces.Services
{
    public interface ILotteryFileService
    {
        Task<List<LotteryFileGetDTO>> GetAllAsync();
        Task<LotteryFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(LotteryFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, LotteryFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
