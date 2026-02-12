using CB.Application.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CB.Infrastructure.Services
{
    public class LocalFileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public LocalFileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(IFormFile file, string folder, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Fayl boşdur.");

            // wwwroot/uploads/{folder}
            var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadsRoot);

            var ext = Path.GetExtension(file.FileName) ?? "";
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(uploadsRoot, fileName);

            await using var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(fs, ct);

            // DB-də saxlanacaq relative path
            var relative = Path.Combine("uploads", folder, fileName).Replace("\\", "/");
            return relative;
        }

        public void Delete(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            var cleaned = relativePath.TrimStart('/', '\\');
            var fullPath = Path.Combine(_env.WebRootPath, cleaned);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
