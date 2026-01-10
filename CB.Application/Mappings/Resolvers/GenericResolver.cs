using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace CB.Application.Mappings.Resolvers
{
    public class GenericResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string?>
    where TSource : class
    where TDestination : class
    {
        private readonly IHttpContextAccessor _accessor;
        public GenericResolver(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string? Resolve(TSource source, TDestination destination, string? destMember, ResolutionContext context)
        {
            string[] propertyNames = new[] { "Icon","Url", "Image", "HeaderLogo", "FooterLogo", "Favicon","File", "PdfFile", "ReceptionSchedule", "Statute" };
            HttpRequest? request = _accessor.HttpContext?.Request;
            string baseUrl = request != null
                ? $"{request.Scheme}://{request.Host}"
                : "https://admin.tecnol.az";

            Type? sourceType = typeof(TSource);
            PropertyInfo? propertyName = propertyNames.Select(prop => sourceType?.GetProperty(prop)).FirstOrDefault(p => p != null);
            string? imageValue = propertyName?.GetValue(source)?.ToString();

            return string.IsNullOrEmpty(imageValue)
                    ? null
                    : $"{baseUrl}{imageValue}";

        }
    }
}
