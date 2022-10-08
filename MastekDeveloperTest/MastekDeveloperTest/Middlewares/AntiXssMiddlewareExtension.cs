using Microsoft.AspNetCore.Builder;

namespace MastekDeveloperTest.Middlewares
{
    /// <summary>
    /// Anti-Xss Middleware Extension
    /// </summary>
    public static class AntiXssMiddlewareExtension
    {
        /// <summary>
        /// Inject in services middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleware>();
        }
    }
}
