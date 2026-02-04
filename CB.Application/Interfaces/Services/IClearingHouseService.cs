
using CB.Application.DTOs.ClearingHouse;

namespace CB.Application.Interfaces.Services
{
    public interface IClearingHouseService
    {
        Task<bool> CreateOrUpdate(ClearingHousePostDTO dTO);
        Task<ClearingHouseGetDTO?> GetFirst();
    }
}
