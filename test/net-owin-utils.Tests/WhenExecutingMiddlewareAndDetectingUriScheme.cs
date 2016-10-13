using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace net_owin_utils.Tests
{
    public class WhenExecutingMiddlewareAndDetectingUriScheme
    {
        private readonly ProxyForwardedAwarenessMiddleware _middleware;

        public WhenExecutingMiddlewareAndDetectingUriScheme()
        {
            _middleware = new ProxyForwardedAwarenessMiddleware(c => Task.FromResult(0));
        }

        [Fact]
        public void GivenNoForwardHeadersArePresentThenTheSchemeRemainsHttp()
        {
            var httpContext = GivenAHttpContext();

            _middleware.Invoke(httpContext).Wait();

            httpContext
                .Request
                .Scheme
                .Should()
                .Be("http");
        }

        [Fact]
        public void GivenAnXForwardProtoHeaderOfHttpThenTheSchemeRemainsHttp()
        {
            var httpContext = GivenAHttpContext();
            GivenXForwardedProtoHeaderWithValue(httpContext, "http");

            _middleware.Invoke(httpContext).Wait();

            httpContext
                .Request
                .Scheme
                .Should()
                .Be("http");
        }

        [Fact]
        public void GivenAnXForwardProtoHeaderOfHttpsThenTheSchemeRemainsHttp()
        {
            var httpContext = GivenAHttpContext();
            GivenXForwardedProtoHeaderWithValue(httpContext, "https");

            _middleware.Invoke(httpContext).Wait();

            httpContext
                .Request
                .Scheme
                .Should()
                .Be("https");
        }

        private HttpContext GivenAHttpContext()
        {
            var context = new DefaultHttpContext();
            context.Request.Scheme = "http";

            return context;
        }

        private static void GivenXForwardedProtoHeaderWithValue(HttpContext httpContext, string value)
        {
            httpContext.Request.Headers.Add("X-Forwarded-Proto", value);
        }
    }
}