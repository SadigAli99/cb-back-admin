
using CB.Application.DTOs.ComplaintIndex;

namespace CB.Application.Interfaces.Services
{
    public interface IComplaintIndexService
    {
        Task<List<ComplaintIndexGetDTO>> GetAllAsync();
        Task<ComplaintIndexGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ComplaintIndexCreateDTO dto);
        Task<bool> UpdateAsync(int id, ComplaintIndexEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
