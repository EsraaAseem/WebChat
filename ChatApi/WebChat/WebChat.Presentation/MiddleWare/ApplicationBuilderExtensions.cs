using Microsoft.AspNetCore.Builder;

namespace WebChat.Presentation.MiddleWare
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalExceptionGlobalHandler(this IApplicationBuilder app)
           => app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
