using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CB.Shared.Storage
{
    public interface IFileStorage
    {
        Task UploadAsync(string key, Stream content, string contentType, CancellationToken ct = default);
        Task DeleteAsync(string key, CancellationToken ct = default);
        string GetPublicUrl(string key);
    }
}
