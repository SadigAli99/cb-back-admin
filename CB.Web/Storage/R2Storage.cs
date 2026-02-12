using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using CB.Shared.Storage;
using Microsoft.Extensions.Options;

namespace CB.Web.Storage
{
    public sealed class R2Options
    {
        public string ServiceUrl { get; set; } = default!;     // https://<account_id>.r2.cloudflarestorage.com
        public string Bucket { get; set; } = default!;
        public string AccessKeyId { get; set; } = default!;
        public string SecretAccessKey { get; set; } = default!;
        public string? PublicBaseUrl { get; set; }             // https://<your-domain> OR https://<bucket>.r2.dev
    }

    public sealed class R2FileStorage : IFileStorage
    {
        private readonly IAmazonS3 _s3;
        private readonly R2Options _opt;

        public R2FileStorage(IAmazonS3 s3, IOptions<R2Options> opt)
        {
            _s3 = s3;
            _opt = opt.Value;
        }

        public async Task UploadAsync(string key, Stream content, string contentType, CancellationToken ct = default)
        {
            key = NormalizeKey(key);

            if (content.CanSeek) content.Position = 0;

            var req = new PutObjectRequest
            {
                BucketName = _opt.Bucket,
                Key = key,
                InputStream = content,
                ContentType = string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType,

                // Cloudflare R2 üçün vacib:
                DisablePayloadSigning = true,
                DisableDefaultChecksumValidation = true,
            };

            req.Headers.CacheControl = "public, max-age=31536000, immutable";

            await _s3.PutObjectAsync(req, ct);
        }

        public Task DeleteAsync(string key, CancellationToken ct = default)
            => _s3.DeleteObjectAsync(_opt.Bucket, NormalizeKey(key), ct);

        public string GetPublicUrl(string key)
        {
            key = NormalizeKey(key);

            if (!string.IsNullOrWhiteSpace(_opt.PublicBaseUrl))
                return $"{_opt.PublicBaseUrl.TrimEnd('/')}/{key.TrimStart('/')}";

            return key;
        }

        private static string NormalizeKey(string key)
            => key.TrimStart('/').Replace("\\", "/");
    }

    public static class R2StorageServiceCollectionExtensions
    {
        public static IServiceCollection AddR2Storage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<R2Options>(configuration.GetSection("R2"));

            services.AddSingleton<IAmazonS3>(sp =>
            {
                var opt = sp.GetRequiredService<IOptions<R2Options>>().Value;

                var creds = new BasicAWSCredentials(opt.AccessKeyId, opt.SecretAccessKey);
                var cfg = new AmazonS3Config
                {
                    ServiceURL = opt.ServiceUrl,
                    ForcePathStyle = true
                };

                return new AmazonS3Client(creds, cfg);
            });

            services.AddSingleton<IFileStorage, R2FileStorage>();
            return services;
        }
    }
}
