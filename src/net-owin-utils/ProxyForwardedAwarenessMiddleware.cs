using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace net_owin_utils
{
    public class ProxyForwardedAwarenessMiddleware
    {
        private const string XForwardedProto = "X-Forwarded-Proto";
        private readonly RequestDelegate _next;

        public ProxyForwardedAwarenessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(XForwardedProto) &&
                context.Request.Headers[XForwardedProto] == "https")
            {
                context.Request.Scheme = "https";
            }

            await _next.Invoke(context);
        }
    }
}
