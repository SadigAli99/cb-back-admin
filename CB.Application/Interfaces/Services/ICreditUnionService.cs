
using CB.Application.DTOs.CreditUnion;

namespace CB.Application.Interfaces.Services
{
    public interface ICreditUnionService
    {
        Task<List<CreditUnionGetDTO>> GetAllAsync();
        Task<CreditUnionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreditUnionCreateDTO dto);
        Task<bool> UpdateAsync(int id, CreditUnionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
