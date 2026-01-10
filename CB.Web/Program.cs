using CB.Infrastructure;
using CB.Web.Middlewares;
using CB.Web.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Services
// builder.Host.UseSerilog((context, services, configuration) =>
// {
//     configuration.ReadFrom.Configuration(context.Configuration)
//                  .ReadFrom.Services(services)
//                  .Enrich.FromLogContext();
// });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<GenericSchemaFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token-i 'Bearer {token}' formatında yazın."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new List<string>()
    }
});

});


builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// app.UseSerilogRequestLogging();

// Pipeline

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CB API v1");
    c.RoutePrefix = string.Empty;
    c.ConfigObject.PersistAuthorization = true;
});



app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CorsMiddleware>();

app.MapControllers();

app.Run();
