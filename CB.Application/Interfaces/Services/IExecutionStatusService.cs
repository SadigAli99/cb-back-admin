
using CB.Application.DTOs.ExecutionStatus;

namespace CB.Application.Interfaces.Services
{
    public interface IExecutionStatusService
    {
        Task<List<ExecutionStatusGetDTO>> GetAllAsync();
        Task<ExecutionStatusGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ExecutionStatusCreateDTO dto);
        Task<bool> UpdateAsync(int id, ExecutionStatusEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
