
using CB.Application.DTOs.InstantPaymentFAQ;

namespace CB.Application.Interfaces.Services
{
    public interface IInstantPaymentFAQService
    {
        Task<List<InstantPaymentFAQGetDTO>> GetAllAsync();
        Task<InstantPaymentFAQGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InstantPaymentFAQCreateDTO dto);
        Task<bool> UpdateAsync(int id, InstantPaymentFAQEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
