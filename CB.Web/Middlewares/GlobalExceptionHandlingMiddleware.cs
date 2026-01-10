using System.Net;
using System.Text.Json;
using CB.Application.Interfaces.Services;
using Serilog; // ITranslateService üçün

namespace CB.Web.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next
        )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITranslateService translateService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error("Xəta baş verdi : " + ex);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var lang = context.Request.Headers["Accept-Language"].FirstOrDefault() ?? "az";

                string messageKey = context.Response.StatusCode switch
                {
                    StatusCodes.Status400BadRequest => "BadRequest",
                    StatusCodes.Status401Unauthorized => "Unauthorized",
                    StatusCodes.Status403Forbidden => "Forbidden",
                    StatusCodes.Status404NotFound => "NotFound",
                    StatusCodes.Status500InternalServerError => ex.Message,
                    _ => "UnexpectedError"
                };

                var message = await translateService.GetValueAsync(messageKey, lang);

                if (string.IsNullOrEmpty(message))
                {
                    message = messageKey switch
                    {
                        "BadRequest" => lang == "en" ? "An error occurred." : "Xəta baş verdi.",
                        "Unauthorized" => lang == "en" ? "Invalid or missing token." : "Token etibarsızdır və ya göndərilməyib.",
                        "Forbidden" => lang == "en" ? "Access denied." : "İcazə yoxdur.",
                        "NotFound" => lang == "en" ? "Data not found." : "Məlumat tapılmadı.",
                        "ServerError" => lang == "en" ? "Internal server error." : "Server xətası baş verdi.",
                        _ => lang == "en" ? "Unexpected error occurred." : "Gözlənilməz xəta baş verdi."
                    };
                }

                var response = new
                {
                    status = "error",
                    message
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }
    }
}
