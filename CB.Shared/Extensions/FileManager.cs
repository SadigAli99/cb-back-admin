using ImageMagick;
using Microsoft.AspNetCore.Http;
using CB.Shared.Storage;   // <-- bunu əlavə et

namespace CB.Shared.Extensions
{
    public static class FileManager
    {
        public static async Task<string> FileUpload(
            this IFormFile? file,
            string rootFolder,
            string folder,
            IFileStorage storage)   // <-- yeni parametr
        {
            string[] extensions = [".jpg", ".jpeg", ".png", ".bmp", ".gif"];

            if (file == null || file.Length == 0)
                throw new ArgumentException("Fayl düzgün deyil");

            string extension = Path.GetExtension(file.FileName).ToLower();
            string fileName = $"{Guid.NewGuid()}";

            // R2 key: uploads/<folder>/<file>
            string key;

            if (extensions.Contains(extension))
            {
                key = $"uploads/{folder}/{fileName}.webp";

                using var input = file.OpenReadStream();
                using var image = new MagickImage(input);
                image.Format = MagickFormat.WebP;

                await using var outStream = new MemoryStream();
                image.Write(outStream);
                outStream.Position = 0;

                await storage.UploadAsync(key, outStream, "image/webp");
                return storage.GetPublicUrl(key);
            }
            else
            {
                key = $"uploads/{folder}/{fileName}{extension}";

                await using var stream = file.OpenReadStream();
                await storage.UploadAsync(key, stream, file.ContentType ?? "application/octet-stream");
                return storage.GetPublicUrl(key);
            }
        }

        public static void FileDelete(string rootFolder, string fileName)
        {
            string fullPath = Path.Combine(rootFolder, fileName.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
