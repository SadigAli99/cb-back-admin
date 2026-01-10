using CB.Infrastructure.Persistance;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CB.Web.Swagger
{
    public class GenericSchemaFilter : ISchemaFilter
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GenericSchemaFilter(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Dictionary<string, string>))
            {
                schema.AdditionalProperties = null;
                schema.Properties.Clear();

                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var languageCodes = db.Languages.Select(x => x.Code).ToList();

                foreach (string code in languageCodes)
                {
                    schema.Properties.Add(code, new OpenApiSchema
                    {
                        Type = "string",
                        Example = new Microsoft.OpenApi.Any.OpenApiString("")
                    });
                }
            }
        }
    }
}
