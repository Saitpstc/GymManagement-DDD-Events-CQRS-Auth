namespace Authorization_Authentication.Middlewares;

using Microsoft.AspNetCore.Builder;

public static class CustomMiddlewares
{
    public static IApplicationBuilder UseCustomMiddlewares(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}