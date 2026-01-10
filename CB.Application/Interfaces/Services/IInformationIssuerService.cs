
using CB.Application.DTOs.InformationIssuer;

namespace CB.Application.Interfaces.Services
{
    public interface IInformationIssuerService
    {
        Task<List<InformationIssuerGetDTO>> GetAllAsync();
        Task<InformationIssuerGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InformationIssuerCreateDTO dto);
        Task<bool> UpdateAsync(int id, InformationIssuerEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
