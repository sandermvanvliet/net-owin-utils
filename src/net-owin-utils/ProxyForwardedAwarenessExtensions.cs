using Microsoft.AspNetCore.Builder;

namespace net_owin_utils
{
    public static class ProxyForwardedAwarenessExtensions
    {
        public static IApplicationBuilder UseProxyForwardedAwareness(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProxyForwardedAwarenessMiddleware>();
        }
    }
}