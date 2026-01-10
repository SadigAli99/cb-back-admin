
using CB.Application.DTOs.StructureCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IStructureCaptionService
    {
        Task<bool> CreateOrUpdate(StructureCaptionPostDTO dTO);
        Task<StructureCaptionGetDTO> GetFirst();
    }
}
