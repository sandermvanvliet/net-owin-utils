using System.Linq;
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
            // Check that the X-Forwarded-Proto header was sent to us and
            // that it contains HTTPS
            if (context.Request.Headers.ContainsKey(XForwardedProto) &&
                context.Request.Headers[XForwardedProto].Single().ToLower() == "https")
            {
                // If so manipulate the current HttpRequest to trick ASP.Net into thinking it runs under HTTPS
                context.Request.Scheme = "https";
            }

            await _next.Invoke(context);
        }
    }
}
