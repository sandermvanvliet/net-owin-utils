# Cookie authentication behind a load balancer
This repo contains an approach to handle cookie auth where the application is behind a load balancer and only listens on HTTP:

[ Client ] --- HTTPS --> [ Load Balancer ] --- HTTP ---> [ Web app ]

In this scenario when authentication takes place in the web app the client is redirected to the login page. Unfortunately because the web app doesn't know that it is exposed over HTTPS the client gets redirected to the wrong URL.

Fortunately HAProxy (the load balancer) uses the X-Forwarded-Proto header to indicate the externally used URI scheme (HTTP or HTTPS). We can use this to tell ASP.Net how to build the redirect URL.

In the project `src/net-owin-utils` project there are two classes that implement a middleware component to set the `Scheme` property of the `HttpRequest` so that the cookie authentication handler can correctly build the URL.

The project `src/net-owin-utils-sample` contains a demonstration of usage. See the `Startup.cs` file.

Tests for `net-owin-utils` can be found in `test/net-owin-utils.Tests`