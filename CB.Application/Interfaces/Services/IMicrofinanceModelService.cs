
using CB.Application.DTOs.MicrofinanceModel;

namespace CB.Application.Interfaces.Services
{
    public interface IMicrofinanceModelService
    {
        Task<List<MicrofinanceModelGetDTO>> GetAllAsync();
        Task<MicrofinanceModelGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MicrofinanceModelCreateDTO dto);
        Task<bool> UpdateAsync(int id, MicrofinanceModelEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
