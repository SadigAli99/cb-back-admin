
using CB.Application.DTOs.Advertisement;

namespace CB.Application.Interfaces.Services
{
    public interface IAdvertisementService
    {
        Task<List<AdvertisementGetDTO>> GetAllAsync();
        Task<AdvertisementGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(AdvertisementCreateDTO dto);
        Task<bool> UpdateAsync(int id, AdvertisementEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
