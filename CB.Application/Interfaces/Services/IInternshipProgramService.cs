
using CB.Application.DTOs.InternshipProgram;

namespace CB.Application.Interfaces.Services
{
    public interface IInternshipProgramService
    {
        Task<bool> CreateOrUpdate(InternshipProgramPostDTO dTO);
        Task<InternshipProgramGetDTO> GetFirst();
    }
}
