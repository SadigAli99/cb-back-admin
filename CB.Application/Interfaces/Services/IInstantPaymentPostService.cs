
using CB.Application.DTOs.InstantPaymentPost;

namespace CB.Application.Interfaces.Services
{
    public interface IInstantPaymentPostService
    {
        Task<List<InstantPaymentPostGetDTO>> GetAllAsync();
        Task<InstantPaymentPostGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InstantPaymentPostCreateDTO dto);
        Task<bool> UpdateAsync(int id, InstantPaymentPostEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
