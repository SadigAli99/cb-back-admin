

using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace CB.Shared.Extensions
{
    public static class FileManager
    {
        public static async Task<string> FileUpload(this IFormFile? file, string rootFolder, string folder)
        {
            string[] extensions = [".jpg", ".jpeg", ".png", ".bmp", ".gif"];

            if (file == null || file.Length == 0)
                throw new ArgumentException("Fayl düzgün deyil");

            string uploadPath = Path.Combine(rootFolder, "uploads", folder);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string extension = Path.GetExtension(file.FileName).ToLower();
            string fileName = $"{Guid.NewGuid()}";
            string finalPath;

            if (extensions.Contains(extension))
            {
                // WebP formatına çevir
                finalPath = Path.Combine(uploadPath, fileName + ".webp");
                using (var stream = file.OpenReadStream())
                using (var image = new MagickImage(stream))
                {
                    image.Format = MagickFormat.WebP;
                    image.Write(finalPath);
                }

                return $"/uploads/{folder}/{fileName}.webp";
            }
            else
            {
                // Normal fayl kimi saxla
                finalPath = Path.Combine(uploadPath, fileName + extension);
                using (var stream = new FileStream(finalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return $"/uploads/{folder}/{fileName}{extension}";
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
