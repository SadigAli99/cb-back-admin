
using CB.Application.DTOs.Hero;

namespace CB.Application.Interfaces.Services
{
    public interface IHeroService
    {
        Task<bool> CreateOrUpdate(HeroPostDTO dTO);
        Task<HeroGetDTO?> GetFirst();
    }
}
