using CB.Application.DTOs.IssuerType;

namespace CB.Application.Interfaces.Services
{
    public interface IIssuerTypeService
    {
        Task<List<IssuerTypeGetDTO>> GetAllAsync();
        Task<IssuerTypeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(IssuerTypeCreateDTO dto);
        Task<bool> UpdateAsync(int id, IssuerTypeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
