
using CB.Application.DTOs.DigitalPaymentReview;

namespace CB.Application.Interfaces.Services
{
    public interface IDigitalPaymentReviewService
    {
        Task<List<DigitalPaymentReviewGetDTO>> GetAllAsync();
        Task<DigitalPaymentReviewGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DigitalPaymentReviewCreateDTO dto);
        Task<bool> UpdateAsync(int id, DigitalPaymentReviewEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
