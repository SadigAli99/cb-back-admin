using CB.Application.DTOs.BankSector;

namespace CB.Application.Interfaces.Services
{
    public interface IBankSectorService
    {
        Task<List<BankSectorGetDTO>> GetAllAsync();
        Task<BankSectorGetDTO?> GetByIdAsync(int id);
        Task<BankSectorEditDTO?> GetForEditAsync(int id);
        Task<bool> CreateAsync(BankSectorCreateDTO dto);
        Task<bool> UpdateAsync(int id, BankSectorEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
