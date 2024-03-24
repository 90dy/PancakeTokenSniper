using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace BscTokenSniper.Services
{
    public class WebHostService : IHostedService
    {
        private readonly IWebHost _webHost;

        public WebHostService()
        {
            _webHost = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:8080")
                .Configure(app => app.Run(context =>
                {
                    return context.Response.WriteAsync("Hello, world!");
                }))
                .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _webHost.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _webHost.StopAsync(cancellationToken);
        }
    }
}
