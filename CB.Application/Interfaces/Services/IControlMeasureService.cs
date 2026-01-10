using CB.Application.DTOs.ControlMeasure;

namespace CB.Application.Interfaces.Services
{
    public interface IControlMeasureService
    {
        Task<List<ControlMeasureGetDTO>> GetAllAsync();
        Task<ControlMeasureGetDTO?> GetByIdAsync(int id);
        Task<ControlMeasureEditDTO?> GetForEditAsync(int id);
        Task<bool> CreateAsync(ControlMeasureCreateDTO dto);
        Task<bool> UpdateAsync(int id, ControlMeasureEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
