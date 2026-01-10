
using CB.Application.DTOs.GreenTaxonomy;

namespace CB.Application.Interfaces.Services
{
    public interface IGreenTaxonomyService
    {
        Task<bool> CreateOrUpdate(GreenTaxonomyPostDTO dTO);
        Task<GreenTaxonomyGetDTO> GetFirst();
    }
}
