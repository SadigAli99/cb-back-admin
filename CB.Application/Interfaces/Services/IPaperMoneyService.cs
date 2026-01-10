
using CB.Application.DTOs.PaperMoney;

namespace CB.Application.Interfaces.Services
{
    public interface IPaperMoneyService
    {
        Task<List<PaperMoneyGetDTO>> GetAllAsync();
        Task<PaperMoneyGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaperMoneyCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaperMoneyEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
