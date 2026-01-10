
using CB.Application.DTOs.PaymentStatistic;

namespace CB.Application.Interfaces.Services
{
    public interface IPaymentStatisticService
    {
        Task<List<PaymentStatisticGetDTO>> GetAllAsync();
        Task<PaymentStatisticGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PaymentStatisticCreateDTO dto);
        Task<bool> UpdateAsync(int id, PaymentStatisticEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
