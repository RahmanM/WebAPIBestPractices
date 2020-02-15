using Microsoft.AspNetCore.Builder;

namespace Dotnet.Core.Todos.Api.ExceptionHandler
{
    public static class Extensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
