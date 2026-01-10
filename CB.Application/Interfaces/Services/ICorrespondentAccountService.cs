
using CB.Application.DTOs.CorrespondentAccount;

namespace CB.Application.Interfaces.Services
{
    public interface ICorrespondentAccountService
    {
        Task<List<CorrespondentAccountGetDTO>> GetAllAsync();
        Task<CorrespondentAccountGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CorrespondentAccountCreateDTO dto);
        Task<bool> UpdateAsync(int id, CorrespondentAccountEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
