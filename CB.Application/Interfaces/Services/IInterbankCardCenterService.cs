
using CB.Application.DTOs.InterbankCardCenter;

namespace CB.Application.Interfaces.Services
{
    public interface IInterbankCardCenterService
    {
        Task<bool> CreateOrUpdate(InterbankCardCenterPostDTO dTO);
        Task<InterbankCardCenterGetDTO?> GetFirst();
    }
}
