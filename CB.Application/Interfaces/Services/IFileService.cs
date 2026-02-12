using Microsoft.AspNetCore.Http;

namespace CB.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string folder, CancellationToken ct = default);
        void Delete(string? relativePath);
    }
}
