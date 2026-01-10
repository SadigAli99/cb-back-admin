
using CB.Application.DTOs.InformationMemorandum;

namespace CB.Application.Interfaces.Services
{
    public interface IInformationMemorandumService
    {
        Task<List<InformationMemorandumGetDTO>> GetAllAsync();
        Task<InformationMemorandumGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InformationMemorandumCreateDTO dto);
        Task<bool> UpdateAsync(int id, InformationMemorandumEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
